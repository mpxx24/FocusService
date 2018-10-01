using System.Collections.Generic;
using FocusWcfService.Models;

namespace FocusWcfService.ProcessesHelpers {
    public interface IProcessesListService {
        WatchedProcess GetProcess(string name);
        IEnumerable<WatchedProcess> GetCurrentlyWatchedProcesses();
        void AddWatchedProcess(WatchedProcess process);
        void RemoveWatchedProcess(WatchedProcess process);
        void UpdateTimeForWatchedProcess(WatchedProcess process);
    }
}