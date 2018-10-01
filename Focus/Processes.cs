using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Focus {
    public class Processes : ObservableCollection<ProcessModel> {
        public Processes() {
            foreach (var process in Process.GetProcesses()) {
                this.Add(this.ConvertToProcessViewModel(process));
            }
        }

        private ProcessModel ConvertToProcessViewModel(Process process) {
            var dto = new ProcessModel {
                Id = process.Id,
                Name = process.ProcessName,
                TimePerDay = "-"
            };

            return dto;
        }
    }
}