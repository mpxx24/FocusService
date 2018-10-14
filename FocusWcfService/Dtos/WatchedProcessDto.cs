using System;

namespace FocusWcfService.Dtos {
    public class WatchedProcessDto {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan TimeAllowedPerDay { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public DateTime LastWatchedDate { get; set; }

        public bool IsCurrentlyRunning { get; set; }
    }
}