using System;
using System.Collections.Generic;
using FocusWcfService.Dtos;
using FocusWcfService.ProcessesHelpers;

namespace FocusWcfService {
    public class ProcessesOperationsService : IProcessesOperationsService {
        private readonly IProcessesListSqlLiteService processesListService;
        
        public ProcessesOperationsService(IProcessesListSqlLiteService processesListService) {
            this.processesListService = processesListService;
        }

        public bool KillProcess(string name) {
            return ProcessesHelper.KillProcess(name);
        }

        //public IEnumerable<Process> GetAllRunningProcesses() {
        //    return Process.GetProcesses();
        //}

        public void AddProcessToObservedProcessesList(string processName, TimeSpan allowedTime) {
            this.processesListService.AddWatchedProcess(ProcessesHelper.GetRealProcessName(processName), allowedTime);
        }

        public void RemoveProcessFromObservedProcessesList(string processName) {
            this.processesListService.RemoveWatchedProcess(ProcessesHelper.GetRealProcessName(processName));
        }

        public IEnumerable<WatchedProcessDto> GetAllWatchedProcesses() {
            var processes = this.processesListService.GetCurrentlyRunningWatchedProcesses();
            return processes;
        }

        public void UpdateProcessInObservedProcessesList(string processName, TimeSpan allowedTime) {
            this.processesListService.ChangeAllowedTimeForWatchedProcess(ProcessesHelper.GetRealProcessName(processName), allowedTime);
        }
    }
}