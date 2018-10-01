using System;
using System.Linq;
using System.Management;
using FocusWcfService;
using FocusWcfService.ProcessesHelpers;

namespace FocusWindowsService {
    public class ProcessWatcher  {
        // The dot in the scope means use the current machine
        private static readonly string scope = @"\\.\root\CIMV2";

        private readonly IProcessesListSqlLiteService processesListService;

        public ProcessWatcher(IProcessesListSqlLiteService processesListService) {
            this.processesListService = processesListService;
        }

        public ManagementEventWatcher WatchForProcessStart() {
            //var queryString =
            //    "SELECT TargetInstance" +
            //    "  FROM __InstanceCreationEvent " +
            //    "WITHIN  10 " +
            //    " WHERE TargetInstance ISA 'Win32_Process' " +
            //    "   AND TargetInstance.Name = '" + processName + "'";

            var query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");

            // Create a watcher and listen for events
            var watcher = new ManagementEventWatcher {Query = query};
            watcher.EventArrived += this.ProcessStarted;
            watcher.Start();
            return watcher;
        }

        public ManagementEventWatcher WatchForProcessEnd() {
            //var queryString =
            //    "SELECT TargetInstance" +
            //    "  FROM __InstanceDeletionEvent " +
            //    "WITHIN  10 " +
            //    " WHERE TargetInstance ISA 'Win32_Process' " +
            //    "   AND TargetInstance.Name = '" + processName + "'";

            var query = new WqlEventQuery("__InstanceDeletionEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");

            var watcher = new ManagementEventWatcher {Query = query};
            watcher.EventArrived += this.ProcessEnded;
            watcher.Start();
            return watcher;
        }

        private void ProcessEnded(object sender, EventArrivedEventArgs e) {
            var targetInstance = (ManagementBaseObject) e.NewEvent.Properties["TargetInstance"].Value;
            var processName = targetInstance.Properties["Name"].Value?.ToString();

            var processes = this.processesListService.GetCurrentlyWatchedProcesses();
            if (!string.IsNullOrEmpty(processName) && processes.Any(x => x.Name == processName || processName == $"{x}.exe")) {
                var process = this.processesListService.GetProcess(processName);
                this.processesListService.SetProcessAsCurrentlyWatched(process, true);
            }
        }

        private void ProcessStarted(object sender, EventArrivedEventArgs e) {
            var targetInstance = (ManagementBaseObject) e.NewEvent.Properties["TargetInstance"].Value;
            var processName = targetInstance.Properties["Name"].Value?.ToString();

            var processes = this.processesListService.GetCurrentlyWatchedProcesses();
            if (!string.IsNullOrEmpty(processName) && processes.Any(x => x.Name == processName || processName == $"{x}.exe")) {
                var process = this.processesListService.GetProcess(processName);
                this.processesListService.SetProcessAsCurrentlyWatched(process, false);
            }
        }
    }
}