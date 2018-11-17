using System.Collections.Generic;
using System.ServiceModel;
using Focus.Core.LocationsHelpers;
using FocusWcfService.Dtos;

namespace FocusWcfService {
    [ServiceContract(Namespace = "http://FocusLocationsOperations")]
    public interface ILocationsOperationsService {
        [OperationContract]
        void AddLocationToObservedLocationsList(string path, string fileName, WatchedLocationActionType actionType);

        [OperationContract]
        void RemoveLocationFromObservedLocationsList(string path, string fileName);

        [OperationContract]
        IEnumerable<WatchedLocationDto> GetAllWatchedLocations();
    }
}