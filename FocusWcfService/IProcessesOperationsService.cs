using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

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
    }
}