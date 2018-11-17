using System;
using Focus.Core.LocationsHelpers;

namespace FocusWcfService.Dtos {
    public class WatchedLocationDto {
        public Guid Id { get; set; }

        public string LocationPath { get; set; }

        public string FileName { get; set; }

        public WatchedLocationActionType ActionType { get; set; }
    }
}