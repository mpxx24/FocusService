using System;
using System.Collections.Generic;
using FocusWcfService.Common;
using FocusWcfService.Models;
using FocusWcfService.ProcessesHelpers;

namespace FocusWcfService {
    public class ProcessesOperationsService : IProcessesOperationsService {
        private readonly IProcessesListSqlLiteService processesListService;

        private readonly IWatchedProcessesCache cache;
        
        public ProcessesOperationsService(IProcessesListSqlLiteService processesListService, IWatchedProcessesCache cache) {
            this.processesListService = processesListService;
            this.cache = cache;
        }

        public bool KillProcess(string name) {
            return ProcessesHelper.KillProcess(name);
        }

        //public IEnumerable<Process> GetAllRunningProcesses() {
        //    return Process.GetProcesses();
        //}

        public void AddProcessToObservedProcessesList(string processName, TimeSpan allowedTime) {
            this.processesListService.AddWatchedProcess(processName, allowedTime);
            this.cache.RefreshCache();
        }

        public void RemoveProcessFromObservedProcessesList(string processName) {
            this.processesListService.RemoveWatchedProcess(processName);
            this.cache.RefreshCache();
        }

        public IEnumerable<WatchedProcess> GetAllWatchedProcesses() {
            var processes = this.processesListService.GetCurrentlyWatchedProcesses();
            return processes;
        }

        public void UpdateProcessInObservedProcessesList(string processName, TimeSpan allowedTime) {
            this.processesListService.ChangeAllowedTimeForWatchedProcess(processName, allowedTime);
        }
    }
}