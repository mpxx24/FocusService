using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FocusWcfService.Models;

namespace FocusWindowsService {
    [Obsolete]
    public class WatchedProcesses : IEnumerable<WatchedProcess> {
        private readonly List<WatchedProcess> processesList = new List<WatchedProcess>();

        public IEnumerator<WatchedProcess> GetEnumerator() {
            return this.processesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public void Add(WatchedProcess process) {
            // ReSharper disable once SimplifyLinqExpression
            if (!this.processesList.Any(x => x.Name == process.Name)) {
                this.processesList.Add(process);
            }
        }

        public void AddRange(IEnumerable<WatchedProcess> processes) {
            if (processes != null) {
                this.processesList.AddRange(processes);
            }
        }

        public void Remove(WatchedProcess process) {
            if (this.processesList.Any(x => x.Name == process.Name)) {
                this.processesList.Remove(process);
            }
        }
    }
}