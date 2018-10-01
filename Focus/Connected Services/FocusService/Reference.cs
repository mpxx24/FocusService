﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Focus.FocusService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://FocusProcessesOperations", ConfigurationName="FocusService.IProcessesOperationsService")]
    public interface IProcessesOperationsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/KillProcess", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/KillProcessResponse")]
        bool KillProcess(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/KillProcess", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/KillProcessResponse")]
        System.Threading.Tasks.Task<bool> KillProcessAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/AddProcessToObservedP" +
            "rocessesList", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/AddProcessToObservedP" +
            "rocessesListResponse")]
        void AddProcessToObservedProcessesList(string processName, System.TimeSpan allowedTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/AddProcessToObservedP" +
            "rocessesList", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/AddProcessToObservedP" +
            "rocessesListResponse")]
        System.Threading.Tasks.Task AddProcessToObservedProcessesListAsync(string processName, System.TimeSpan allowedTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/RemoveProcessFromObse" +
            "rvedProcessesList", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/RemoveProcessFromObse" +
            "rvedProcessesListResponse")]
        void RemoveProcessFromObservedProcessesList(string processName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/RemoveProcessFromObse" +
            "rvedProcessesList", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/RemoveProcessFromObse" +
            "rvedProcessesListResponse")]
        System.Threading.Tasks.Task RemoveProcessFromObservedProcessesListAsync(string processName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProcessesOperationsServiceChannel : Focus.FocusService.IProcessesOperationsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProcessesOperationsServiceClient : System.ServiceModel.ClientBase<Focus.FocusService.IProcessesOperationsService>, Focus.FocusService.IProcessesOperationsService {
        
        public ProcessesOperationsServiceClient() {
        }
        
        public ProcessesOperationsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProcessesOperationsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessesOperationsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessesOperationsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool KillProcess(string name) {
            return base.Channel.KillProcess(name);
        }
        
        public System.Threading.Tasks.Task<bool> KillProcessAsync(string name) {
            return base.Channel.KillProcessAsync(name);
        }
        
        public void AddProcessToObservedProcessesList(string processName, System.TimeSpan allowedTime) {
            base.Channel.AddProcessToObservedProcessesList(processName, allowedTime);
        }
        
        public System.Threading.Tasks.Task AddProcessToObservedProcessesListAsync(string processName, System.TimeSpan allowedTime) {
            return base.Channel.AddProcessToObservedProcessesListAsync(processName, allowedTime);
        }
        
        public void RemoveProcessFromObservedProcessesList(string processName) {
            base.Channel.RemoveProcessFromObservedProcessesList(processName);
        }
        
        public System.Threading.Tasks.Task RemoveProcessFromObservedProcessesListAsync(string processName) {
            return base.Channel.RemoveProcessFromObservedProcessesListAsync(processName);
        }
    }
}