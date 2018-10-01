using System;

namespace Focus {
    public class ProcessModel {
        private readonly TimeSpan maxTimeSpan = new TimeSpan(24, 0, 0);

        private int id { get; set; }
        private string name { get; set; }
        private TimeSpan timePerDay { get; set; }

        public int Id {
            get => this.id;
            set => this.id = value;
        }

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
    }
}