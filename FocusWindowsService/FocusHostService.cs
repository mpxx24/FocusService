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

            this.serviceHost?.Close();
            this.serviceHost = new ServiceHost(this.processesOperationsService);
            var behaviour = this.serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behaviour.InstanceContextMode = InstanceContextMode.Single;
            this.serviceHost.Open();

            this.StartWatcherTimer();

            this.StartWatchingForProcessesFromTheList();
        }

        private void StartWatcherTimer() {
            var statusTime = new Timer {Interval = 10000};
            statusTime.Elapsed += this.OnTimerTick;
            statusTime.Enabled = true;
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e) {
            var processes = this.processesListService.GetCurrentlyWatchedProcesses();
            foreach (var watchedProcess in processes) {
                this.processesListService.UpdateTimeForWatchedProcess(watchedProcess);
            }
        }

        protected override void OnStop() {
            if (this.serviceHost != null) {
                this.serviceHost.Close();
                this.serviceHost = null;
            }
        }

        private void StartWatchingForProcessesFromTheList() {
            var processWatcher = new ProcessWatcher(this.processesListService);
            processWatcher.WatchForProcessStart();
            processWatcher.WatchForProcessEnd();
        }

        //private void StartWatchingForProcessesFromTheList() {
        //    InitialListToWatch = this.processesListService.GetCurrentlyWatchedProcesses().ToList();

        //    var alreadyRunningProcessesFromList = Process.GetProcesses()
        //                                                 .Where(x => InitialListToWatch.Any(y => x.ProcessName == y))
        //                                                 .ToList()
        //                                                 .Distinct();

        //    foreach (var process in alreadyRunningProcessesFromList) {
        //        //remove the extension, lol
        //        WatchedProcesses.Add(Path.GetFileName(process.ProcessName));
        //    }

        //    ProcessWatcher.WatchForProcessStart();
        //    ProcessWatcher.WatchForProcessEnd();
        //}
    }
}