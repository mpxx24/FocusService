using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceProcess;
using System.Timers;
using FocusWcfService;
using FocusWcfService.Models;
using FocusWcfService.ProcessesHelpers;

namespace FocusWindowsService {
    public partial class FocusHostService : ServiceBase {
        protected static List<WatchedProcess> InitialListToWatch = new List<WatchedProcess>();

        private readonly IProcessesListSqlLiteService processesListService;

        private readonly IProcessesOperationsService processesOperationsService;

        private ServiceHost serviceHost;

        public FocusHostService(IProcessesListSqlLiteService processesListService, IProcessesOperationsService processesOperationsService) {
            this.processesListService = processesListService;
            this.processesOperationsService = processesOperationsService;

            this.InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            //Debugger.Launch();

            this.StartHostingWcfService(this.processesOperationsService);

            this.StartWatcherTimer();
        }

        private void StartHostingWcfService(object operationsService) {
            this.serviceHost?.Close();
            this.serviceHost = new ServiceHost(operationsService);
            var behaviour = this.serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behaviour.InstanceContextMode = InstanceContextMode.Single;
            this.serviceHost.Open();
        }

        private void StartWatcherTimer() {
            var statusTime = new Timer {Interval = 60000};
            statusTime.Elapsed += this.OnTimerTick;
            statusTime.Enabled = true;
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e) {
            var processes = this.processesListService.GetAllWatchedProcesses();

            foreach (var watchedProcess in processes) {
                this.processesListService.UpdateTimeForWatchedProcess(watchedProcess.Name);
            }
        }

        protected override void OnStop() {
            if (this.serviceHost != null) {
                this.serviceHost.Close();
                this.serviceHost = null;
            }
        }
    }
}