using FluentNHibernate.Mapping;

namespace FocusWcfService.Models {
    public class WatchedProcessMap : ClassMap<WatchedProcess> {
        public WatchedProcessMap() {
            this.Table("WatchedProcesses");
            this.Id(x => x.Id).Column("uId");
            this.Map(x => x.Name).Column("vName");
            this.Map(x => x.TimeAllowedPerDay).Column("tsAllowedTime");
            this.Map(x => x.TimeLeft).Column("tsTimeLeft");
            this.Map(x => x.LastWatchedDate).Column("dtLastWatchedDate");
        }
    }
}