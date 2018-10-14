using System;

namespace FocusWcfService.Models {
    public class WatchedProcess {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual TimeSpan TimeAllowedPerDay { get; set; }
        public virtual TimeSpan TimeLeft { get; set; }
        public virtual DateTime LastWatchedDate { get; set; }
    }
}