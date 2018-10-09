using System;
using System.Collections.Generic;
using System.Linq;
using FocusWcfService.Common;
using FocusWcfService.Models;
using FocusWcfService.ProcessesHelpers;

namespace FocusWcfService {
    public class ProcessesOperationsService : IProcessesOperationsService {
        private readonly IProcessesListSqlLiteService processesListService;

        private readonly IWatchedProcessesCache cache;

        private readonly IRepository<WatchedProcess> repository;

        public ProcessesOperationsService(IProcessesListSqlLiteService processesListService, IRepository<WatchedProcess> repository, IWatchedProcessesCache cache) {
            this.processesListService = processesListService;
            this.repository = repository;
            this.cache = cache;
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
            this.cache.RefreshCache();
        }

        public void RemoveProcessFromObservedProcessesList(string processName) {
            var process = this.repository.Filter<WatchedProcess>(x => x.Name == processName).FirstOrDefault();
            this.processesListService.RemoveWatchedProcess(process);
            this.cache.RefreshCache();
        }

        public IEnumerable<WatchedProcess> GetAllWatchedProcesses() {
            var processes = this.processesListService.GetCurrentlyWatchedProcesses();
            return processes;
        }
    }
}