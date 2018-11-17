using System.Collections.Generic;
using Focus.Core.LocationsHelpers;
using FocusWcfService.Dtos;

namespace FocusWcfService.LocationsHelpers {
    public interface ILocationsListSqlLiteService {
        void AddWatchedLocationAndFile(string locationPath, string fileName, WatchedLocationActionType actionType);
        void RemoveWatchedLocationAndFile(string locationPath, string fileName);
        IEnumerable<WatchedLocationDto> GetAllWatchedLocations();
    }
}