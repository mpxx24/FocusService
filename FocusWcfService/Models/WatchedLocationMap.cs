using FluentNHibernate.Mapping;

namespace FocusWcfService.Models {
    public class WatchedLocationMap : ClassMap<WatchedLocation> {
        public WatchedLocationMap() {
            this.Table("WatchedLocations");
            this.Id(x => x.Id).Column("uId");
            this.Map(x => x.FileName).Column("vFileName");
            this.Map(x => x.Path).Column("vFilePath");
            this.Map(x => x.ActionType).Column("tiActionType");
        }
    }
}