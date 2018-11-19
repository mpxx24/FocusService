using System.ServiceModel;
using System.ServiceProcess;
using System.Timers;
using FocusWcfService;
using FocusWcfService.ProcessesHelpers;
using FocusWindowsService.FilesHelpers;
using NLog;

namespace FocusWindowsService {
    public partial class FocusHostService : ServiceBase {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly IProcessesListSqlLiteService processesListService;

        private readonly IProcessesOperationsService processesOperationsService;

        private readonly ILocationsOperationsService locationsOperationsService;

        private ServiceHost serviceHost;

        public FocusHostService(IProcessesListSqlLiteService processesListService, IProcessesOperationsService processesOperationsService, ILocationsOperationsService locationsOperationsService) {
            this.processesListService = processesListService;
            this.processesOperationsService = processesOperationsService;
            this.locationsOperationsService = locationsOperationsService;

            this.InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            //Debugger.Launch();

            this.StartHostingWcfService(this.processesOperationsService);
            this.StartWatchingLocations();
            this.StartWatcherTimer();
        }

        private void StartWatchingLocations() {
            var allLocations = this.locationsOperationsService.GetAllWatchedLocations();
            foreach (var watchedLocationDto in allLocations) {
                FilesWatcherHelper.StartFilesWatcher(watchedLocationDto);
            }
        }

        private void StartHostingWcfService(object operationsService) {
            this.logger.Debug("Trying to open new service host. Hosting WCF service.");

            this.serviceHost?.Close();
            this.serviceHost = new ServiceHost(operationsService);
            var behaviour = this.serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behaviour.InstanceContextMode = InstanceContextMode.Single;
            this.serviceHost.Open();

            this.logger.Debug("Succesfully opened new service host.");
        }

        private void StartWatcherTimer() {
            var interval = 60000;

            this.logger.Debug($"Starting service timer! Interval: {interval}");

            var statusTime = new Timer {Interval = interval};
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
            this.logger.Debug("Stopping windows service!");

            if (this.serviceHost != null) {
                this.logger.Debug("Stopping to host WCF service!");

                this.serviceHost.Close();
                this.serviceHost = null;
            }
        }
    }
}