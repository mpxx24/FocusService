using System;
using System.Collections.Generic;
using FocusWcfService.Models;

namespace FocusWcfService.ProcessesHelpers {
    public interface IProcessesListService {
        WatchedProcess GetProcess(string name);
        IEnumerable<WatchedProcess> GetCurrentlyWatchedProcesses();
        void AddWatchedProcess(string processName, TimeSpan allowedTime);
        void RemoveWatchedProcess(string processName);
        void UpdateTimeForWatchedProcess(string processName);
    }
}