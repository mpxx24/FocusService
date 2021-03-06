﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FocusWcfService.Common;
using FocusWcfService.Dtos;
using FocusWcfService.Models;
using NLog;

namespace FocusWcfService.Services.Processes {
    public class ProcessesListSqlLiteService : IProcessesListSqlLiteService {
        private readonly IRepository<WatchedProcess> repository;

        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public ProcessesListSqlLiteService(IRepository<WatchedProcess> repository) {
            this.repository = repository;
        }

        public WatchedProcessDto GetProcess(string name) {
            var process = this.GetWatchedProcess(name);
            return MapWatchedProcessToDto(process);
        }

        public IEnumerable<WatchedProcessDto> GetAllWatchedProcesses() {
            var watchedProcesses = this.repository.GetAll().ToList();
            this.logger.Debug($"Found {watchedProcesses.Count} watched process(es).");
            watchedProcesses.ForEach(x => {
                                         this.logger.Debug($"\t'{x.Name}' - {x.TimeAllowedPerDay} - {x.TimeLeft}");
                                     });

            return this.MapWatchedProcessToDtos(watchedProcesses);
        }

        public void AddWatchedProcess(string processName, TimeSpan allowedTime) {
            var isProcessAlreadyWatched = this.IsProcessWithTheSameNameAlreadyWatched(processName);

            if (isProcessAlreadyWatched.HasValue && isProcessAlreadyWatched.Value) {
                this.logger.Debug($"Process named '{processName}' was already watched! Updating it.");
                this.ChangeAllowedTimeForWatchedProcess(processName, allowedTime);
                return;
            }

            var process = new WatchedProcess {
                Id = Guid.NewGuid(),
                Name = processName,
                TimeAllowedPerDay = allowedTime,
                TimeLeft = allowedTime,
                LastWatchedDate = DateTime.Today.Date
            };

            this.repository.Save(process);
            this.logger.Debug($"Added new watched process '{process.Name}' - {process.TimeAllowedPerDay}.");
        }

        public void RemoveWatchedProcess(string processName) {
            var process = this.repository.Filter<WatchedProcess>(x => x.Name == processName).FirstOrDefault();

            if (process == null) {
                throw new InvalidDataException("Process does not exist in the database!");
            }
            this.repository.Delete(process);
            this.logger.Debug($"Removed process '{processName}' from watched proceses!");
        }

        public bool? IsProcessWithTheSameNameAlreadyWatched(string processName) {
            if (string.IsNullOrEmpty(processName)) {
                throw new InvalidDataException("Process name can not be null or empty");
            }

            var isProcessAlreadyWatched = this.repository.Filter<WatchedProcess>(x => x.Name == processName).Any();
            return isProcessAlreadyWatched;
        }

        public void ChangeAllowedTimeForWatchedProcess(string processName, TimeSpan allowedTime) {
            var process = this.repository.Filter<WatchedProcess>(x => x.Name == processName).FirstOrDefault();

            if (process == null) {
                throw new InvalidDataException("Process does not exist in the database!");
            }
            this.logger.Debug($"Changing allowed time for process '{process.Name}' from {process.TimeAllowedPerDay} to {allowedTime}.");
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
                this.logger.Debug($"Updated watched process '{process.Name}' - last watched: {process.LastWatchedDate.ToShortDateString()}, time left: {process.TimeLeft}.");
                return;
            }

            if (!ProcessesHelper.IsProcessRunning(processName)) {
                return;
            }

            process.TimeLeft = process.TimeLeft.Add(TimeSpan.FromMinutes(-1));
            if (process.TimeLeft < new TimeSpan()) {
                ProcessesHelper.KillProcess(process.Name);
                process.TimeLeft = new TimeSpan();
                this.repository.Update(process);
                this.logger.Debug($"Killed process '{process.Name}'.");
            }
            else {
                this.repository.Update(process);
            }
            this.logger.Debug($"Updated watched process '{process.Name}' - last watched: {process.LastWatchedDate.ToShortDateString()}, time left: {process.TimeLeft}.");
        }

        private WatchedProcess GetWatchedProcess(string name) {
            return this.repository.Filter<WatchedProcess>(x => x.Name == name).FirstOrDefault();
        }

        private IEnumerable<WatchedProcessDto> MapWatchedProcessToDtos(IEnumerable<WatchedProcess> watchedProcesses) {
            return watchedProcesses.Select(MapWatchedProcessToDto);
        }

        private static WatchedProcessDto MapWatchedProcessToDto(WatchedProcess watchedProcess) {
            return new WatchedProcessDto {
                Id = watchedProcess.Id,
                Name = watchedProcess.Name,
                TimeAllowedPerDay = watchedProcess.TimeAllowedPerDay,
                TimeLeft = watchedProcess.TimeLeft,
                LastWatchedDate = watchedProcess.LastWatchedDate,
                IsCurrentlyRunning = ProcessesHelper.IsProcessRunning(watchedProcess.Name)
            };
        }
    }
}