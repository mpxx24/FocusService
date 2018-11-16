using System.ServiceModel;
using FocusWcfService.LocationsHelpers;

namespace FocusWcfService {
    [ServiceContract(Namespace = "http://FocusLocationsOperations")]
    public interface ILocationsOperationsService {
        [OperationContract]
        void AddLocationToObservedLocationsList(string path, string fileName, WatchedLocationActionType actionType);

        [OperationContract]
        void RemoveLocationFromObservedLocationsList(string path, string fileName);
    }
}