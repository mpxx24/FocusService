using System;
using System.Collections.Generic;
using FocusWcfService.Dtos;
using FocusWcfService.Models;

namespace FocusWcfService.ProcessesHelpers {
    public interface IProcessesListSqlLiteService {
        WatchedProcessDto GetProcess(string name);
        IEnumerable<WatchedProcessDto> GetAllWatchedProcesses();
        void AddWatchedProcess(string processName, TimeSpan allowedTime);
        void RemoveWatchedProcess(string processName);
        void UpdateTimeForWatchedProcess(string processName);
        bool? IsProcessWithTheSameNameAlreadyWatched(string processName);
        void ChangeAllowedTimeForWatchedProcess(string processName, TimeSpan allowedTime);
    }
}