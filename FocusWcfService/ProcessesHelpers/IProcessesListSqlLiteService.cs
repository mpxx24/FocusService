using System;
using System.Collections.Generic;
using FocusWcfService.Dtos;
using FocusWcfService.Models;

namespace FocusWcfService.ProcessesHelpers {
    public interface IProcessesListSqlLiteService {
        WatchedProcess GetProcess(string name);
        IEnumerable<WatchedProcessDto> GetCurrentlyRunningWatchedProcesses();
        IEnumerable<WatchedProcessDto> GetAllWatchedProcesses();
        void AddWatchedProcess(string processName, TimeSpan allowedTime);
        void RemoveWatchedProcess(string processName);
        void UpdateTimeForWatchedProcess(string processName);
        bool? IsProcessWithTheSameNameAlreadyWatched(string processName);
        void SetProcessAsCurrentlyWatchedAsync(string processName, bool isCurrentlyWatched);
        void ChangeAllowedTimeForWatchedProcess(string processName, TimeSpan allowedTime);
    }
}