using System;
using System.Collections.Generic;
using Focus.Core.LocationsHelpers;
using FocusWcfService.Dtos;
using FocusWcfService.Services.Locations;
using FocusWcfService.Services.Processes;

namespace FocusWcfService {
    public class OperationsService : IProcessesOperationsService, ILocationsOperationsService {
        private readonly IProcessesListSqlLiteService processesListService;
        private readonly ILocationsListSqlLiteService locationsListService;
        
        public OperationsService(IProcessesListSqlLiteService processesListService, ILocationsListSqlLiteService locationsListService) {
            this.processesListService = processesListService;
            this.locationsListService = locationsListService;
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
            var processes = this.processesListService.GetAllWatchedProcesses();
            return processes;
        }

        public void UpdateProcessInObservedProcessesList(string processName, TimeSpan allowedTime) {
            this.processesListService.ChangeAllowedTimeForWatchedProcess(ProcessesHelper.GetRealProcessName(processName), allowedTime);
        }

        public void AddLocationToObservedLocationsList(string path, string fileName, WatchedLocationActionType actionType) {
            this.locationsListService.AddWatchedLocationAndFile(path, fileName, actionType);
        }

        public void RemoveLocationFromObservedLocationsList(string path, string fileName) {
            this.locationsListService.RemoveWatchedLocationAndFile(path, fileName);
        }

        public IEnumerable<WatchedLocationDto> GetAllWatchedLocations() {
            return this.locationsListService.GetAllWatchedLocations();
        }
    }
}