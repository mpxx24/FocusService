using System;
using System.Collections.Generic;
using System.ServiceModel;
using FocusWcfService.Dtos;
using FocusWcfService.Models;

namespace FocusWcfService {
    [ServiceContract(Namespace = "http://FocusProcessesOperations")]
    public interface IProcessesOperationsService {
        [OperationContract]
        bool KillProcess(string name);

        //[OperationContract]
        //IEnumerable<Process> GetAllRunningProcesses();

        [OperationContract]
        void AddProcessToObservedProcessesList(string processName, TimeSpan allowedTime);

        [OperationContract]
        void RemoveProcessFromObservedProcessesList(string processName);

        [OperationContract]
        IEnumerable<WatchedProcessDto> GetAllWatchedProcesses();

        [OperationContract]
        void UpdateProcessInObservedProcessesList(string processName, TimeSpan allowedTime);
    }
}