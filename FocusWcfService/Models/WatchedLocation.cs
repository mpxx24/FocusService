using System;
using FocusWcfService.LocationsHelpers;

namespace FocusWcfService.Models {
    public class WatchedLocation {
        public virtual Guid Id { get; set; }

        public virtual string Path { get; set; }

        public virtual string FileName { get; set; }

        public virtual WatchedLocationActionType ActionType { get; set; }
    }
}