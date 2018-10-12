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

        public void AddWatchedProcess(string processName, TimeSpan allowedTime) {
            var process = new WatchedProcess {
                Id = Guid.NewGuid(),
                Name = processName,
                TimeAllowedPerDay = allowedTime,
                TimeLeft = allowedTime,
                IsCurrentlyWatched = true,
                LastWatchedDate = DateTime.Today.Date
            };

            this.repository.Save(process);
        }

        public void RemoveWatchedProcess(string processName) {
            var process = this.repository.Filter<WatchedProcess>(x => x.Name == processName).FirstOrDefault();

            if (process == null) {
                throw new InvalidDataException("Process does not exist in the database!");
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

        public void ChangeAllowedTimeForWatchedProcess(string processName, TimeSpan allowedTime) {
            var process = this.repository.Filter<WatchedProcess>(x => x.Name == processName).FirstOrDefault();

            if (process == null) {
                throw new InvalidDataException("Process does not exist in the database!");
            }

            process.TimeAllowedPerDay = allowedTime;
            process.TimeLeft = allowedTime;
            this.repository.Update(process);
        }

        public void UpdateTimeForWatchedProcess(string processName) {
            var process = this.repository.Filter<WatchedProcess>(x => x.Name == processName).FirstOrDefault();

            if (process == null) {
                throw new InvalidDataException("Process does not exist in the database!");
            }

            if (DateTime.Now.Date != process.LastWatchedDate) {
                process.LastWatchedDate = DateTime.Now.Date;
                process.TimeLeft = process.TimeAllowedPerDay;
                this.repository.Update(process);
                return;
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