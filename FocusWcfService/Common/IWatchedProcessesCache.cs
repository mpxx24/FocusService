using System.Collections.Generic;
using FocusWcfService.Models;

namespace FocusWcfService.Common {
    public interface IWatchedProcessesCache {
        void UpdateCache(IEnumerable<WatchedProcess> watchedProcesses);
        IEnumerable<WatchedProcess> GetCachedProcesses();
    }
}