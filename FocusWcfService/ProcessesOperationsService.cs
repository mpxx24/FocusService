using System;
using System.Linq;
using FocusWcfService.Common;
using FocusWcfService.Models;
using FocusWcfService.ProcessesHelpers;

namespace FocusWcfService {
    public class ProcessesOperationsService : IProcessesOperationsService {
        private readonly IProcessesListSqlLiteService processesListService;

        private readonly IRepository<WatchedProcess> repository;

        public ProcessesOperationsService(IProcessesListSqlLiteService processesListService, IRepository<WatchedProcess> repository) {
            this.processesListService = processesListService;
            this.repository = repository;
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
            var process = this.repository.Filter<WatchedProcess>(x => x.Name == processName).FirstOrDefault();
            this.processesListService.RemoveWatchedProcess(process);
        }
    }
}