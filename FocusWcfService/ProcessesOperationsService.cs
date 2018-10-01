﻿using System;
using FocusWcfService.Models;
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
            var process = new WatchedProcess {
                Id = Guid.NewGuid(),
                Name = processName,
                TimeAllowedPerDay = allowedTime,
                TimeLeft = allowedTime,
                IsCurrentlyWatched = true,
                LastWatchedDate = DateTime.Today.Date
            };
            this.processesListService.AddWatchedProcess(process);
        }

        public void RemoveProcessFromObservedProcessesList(string processName) {
            var process = new WatchedProcess {Name = processName};
            this.processesListService.RemoveWatchedProcess(process);
        }
    }
}