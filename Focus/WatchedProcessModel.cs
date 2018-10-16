using System;

namespace Focus {
    public class WatchedProcessModel {
        private readonly TimeSpan maxTimeSpan = new TimeSpan(24, 0, 0);

        private string name { get; set; }

        private TimeSpan timePerDay { get; set; }

        private TimeSpan timeLeftToday { get; set; }

        private string isProcessRunning { get; set; }

        public string Name {
            get => this.name;
            set => this.name = value;
        }

        public string TimePerDay {
            get => this.timePerDay == new TimeSpan(0, 0, 0) ? "-" : this.timePerDay.ToString("g");
            set {
                if (TimeSpan.TryParse(value, out var valueAsTimespan)) {
                    var timeSpan = valueAsTimespan > this.maxTimeSpan
                        ? this.maxTimeSpan
                        : valueAsTimespan;
                    this.timePerDay = timeSpan;
                }
                else {
                    this.timePerDay = new TimeSpan();
                }
            }
        }

        public string TimeLeftToday {
            get => this.timePerDay == new TimeSpan(0, 0, 0) ? "-" : this.timeLeftToday.ToString("g");
            set {
                if (TimeSpan.TryParse(value, out var valueAsTimespan)) {
                    var timeSpan = valueAsTimespan > this.maxTimeSpan
                        ? this.maxTimeSpan
                        : valueAsTimespan;
                    this.timeLeftToday = timeSpan;
                }
                else {
                    this.timeLeftToday = new TimeSpan();
                }
            }
        }

        public string IsProcessRunning {
            get => this.isProcessRunning;
            set => this.isProcessRunning = value;
        }
    }
}