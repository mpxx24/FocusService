using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FocusWcfService.Common;
using FocusWcfService.Models;

namespace FocusWcfService.ProcessesHelpers {
    public class ProcessesListSqlLiteService : IProcessesListSqlLiteService {
        private readonly IRepository<WatchedProcess> repository;

        public ProcessesListSqlLiteService(IRepository<WatchedProcess> repository) {
            this.repository = repository;
        }

        public WatchedProcess GetProcess(string name) {
            return this.repository.Get(name);
        }

        public IEnumerable<WatchedProcess> GetCurrentlyWatchedProcesses() {
            return this.repository.Filter<WatchedProcess>(x => x.IsCurrentlyWatched);
        }

        public void AddWatchedProcess(WatchedProcess process) {
            if (process == null) {
                throw new InvalidDataException("Process can not be null.");
            }

            this.repository.Save(process);
        }

        public void RemoveWatchedProcess(WatchedProcess process) {
            if (process == null) {
                throw new InvalidDataException("Process can not be null.");
            }

            this.repository.Delete(process);
        }

        public bool? IsProcessWithTheSameNameAlreadyWatched(string processName) {
            if (string.IsNullOrEmpty(processName)) {
                throw new InvalidDataException("Process name can not be null or empty");
            }

            var isProcessAlreadyWatched = this.repository.Filter<WatchedProcess>(x => x.Name == processName).Any();
            return isProcessAlreadyWatched;
        }

        public void SetProcessAsCurrentlyWatched(string procesName, bool isCurrentlyWatched) {
            if (string.IsNullOrEmpty(procesName)) {
                throw new InvalidDataException("Process name cannot be null or empty.");
            }

            var dbEntry = this.GetProcess(procesName);

            if (dbEntry == null) {
                throw new InvalidDataException($"Process with name: {procesName} does not exist in the database.");
            }

            dbEntry.IsCurrentlyWatched = isCurrentlyWatched;
            dbEntry.LastWatchedDate = DateTime.Today.Date;
            this.repository.Update(dbEntry);
        }

        public void UpdateTimeForWatchedProcess(WatchedProcess process) {
            if (process == null) {
                throw new InvalidDataException("Process can not be null.");
            }

            process.TimeLeft = process.TimeLeft.Add(TimeSpan.FromMinutes(-1));
            if (process.TimeLeft < new TimeSpan()) {
                ProcessesHelper.KillProcess(process.Name);
                process.TimeLeft = new TimeSpan();
                this.repository.Update(process);
            }
            else {
                this.repository.Update(process);
            }
        }
    }
}