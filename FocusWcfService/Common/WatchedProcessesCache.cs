using System.Collections.Generic;
using System.Linq;
using FocusWcfService.Models;

namespace FocusWcfService.Common {
    public class WatchedProcessesCache : IWatchedProcessesCache {
        private IList<WatchedProcess> dummyCache = new List<WatchedProcess>();

        public void UpdateCache(IEnumerable<WatchedProcess> watchedProcesses) {
            this.dummyCache = watchedProcesses.ToList();
        }

        public IEnumerable<WatchedProcess> GetCachedProcesses() {
            return this.dummyCache;
        }

        public WatchedProcess GetProcess(string processName) {
            var process = this.dummyCache.FirstOrDefault(x => x.Name == processName);
            return process;
        }
    }
}