﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Focus.FocusProcessesService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WatchedProcessDto", Namespace="http://schemas.datacontract.org/2004/07/FocusWcfService.Dtos")]
    [System.SerializableAttribute()]
    public partial class WatchedProcessDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsCurrentlyRunningField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime LastWatchedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan TimeAllowedPerDayField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan TimeLeftField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsCurrentlyRunning {
            get {
                return this.IsCurrentlyRunningField;
            }
            set {
                if ((this.IsCurrentlyRunningField.Equals(value) != true)) {
                    this.IsCurrentlyRunningField = value;
                    this.RaisePropertyChanged("IsCurrentlyRunning");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LastWatchedDate {
            get {
                return this.LastWatchedDateField;
            }
            set {
                if ((this.LastWatchedDateField.Equals(value) != true)) {
                    this.LastWatchedDateField = value;
                    this.RaisePropertyChanged("LastWatchedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan TimeAllowedPerDay {
            get {
                return this.TimeAllowedPerDayField;
            }
            set {
                if ((this.TimeAllowedPerDayField.Equals(value) != true)) {
                    this.TimeAllowedPerDayField = value;
                    this.RaisePropertyChanged("TimeAllowedPerDay");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan TimeLeft {
            get {
                return this.TimeLeftField;
            }
            set {
                if ((this.TimeLeftField.Equals(value) != true)) {
                    this.TimeLeftField = value;
                    this.RaisePropertyChanged("TimeLeft");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WatchedLocationDto", Namespace="http://schemas.datacontract.org/2004/07/FocusWcfService.Dtos")]
    [System.SerializableAttribute()]
    public partial class WatchedLocationDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Focus.Core.LocationsHelpers.WatchedLocationActionType ActionTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationPathField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Focus.Core.LocationsHelpers.WatchedLocationActionType ActionType {
            get {
                return this.ActionTypeField;
            }
            set {
                if ((this.ActionTypeField.Equals(value) != true)) {
                    this.ActionTypeField = value;
                    this.RaisePropertyChanged("ActionType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LocationPath {
            get {
                return this.LocationPathField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationPathField, value) != true)) {
                    this.LocationPathField = value;
                    this.RaisePropertyChanged("LocationPath");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://FocusProcessesOperations", ConfigurationName="FocusProcessesService.IProcessesOperationsService")]
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/GetAllWatchedProcesse" +
            "s", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/GetAllWatchedProcesse" +
            "sResponse")]
        Focus.FocusProcessesService.WatchedProcessDto[] GetAllWatchedProcesses();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/GetAllWatchedProcesse" +
            "s", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/GetAllWatchedProcesse" +
            "sResponse")]
        System.Threading.Tasks.Task<Focus.FocusProcessesService.WatchedProcessDto[]> GetAllWatchedProcessesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/UpdateProcessInObserv" +
            "edProcessesList", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/UpdateProcessInObserv" +
            "edProcessesListResponse")]
        void UpdateProcessInObservedProcessesList(string processName, System.TimeSpan allowedTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusProcessesOperations/IProcessesOperationsService/UpdateProcessInObserv" +
            "edProcessesList", ReplyAction="http://FocusProcessesOperations/IProcessesOperationsService/UpdateProcessInObserv" +
            "edProcessesListResponse")]
        System.Threading.Tasks.Task UpdateProcessInObservedProcessesListAsync(string processName, System.TimeSpan allowedTime);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProcessesOperationsServiceChannel : Focus.FocusProcessesService.IProcessesOperationsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProcessesOperationsServiceClient : System.ServiceModel.ClientBase<Focus.FocusProcessesService.IProcessesOperationsService>, Focus.FocusProcessesService.IProcessesOperationsService {
        
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
        
        public Focus.FocusProcessesService.WatchedProcessDto[] GetAllWatchedProcesses() {
            return base.Channel.GetAllWatchedProcesses();
        }
        
        public System.Threading.Tasks.Task<Focus.FocusProcessesService.WatchedProcessDto[]> GetAllWatchedProcessesAsync() {
            return base.Channel.GetAllWatchedProcessesAsync();
        }
        
        public void UpdateProcessInObservedProcessesList(string processName, System.TimeSpan allowedTime) {
            base.Channel.UpdateProcessInObservedProcessesList(processName, allowedTime);
        }
        
        public System.Threading.Tasks.Task UpdateProcessInObservedProcessesListAsync(string processName, System.TimeSpan allowedTime) {
            return base.Channel.UpdateProcessInObservedProcessesListAsync(processName, allowedTime);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://FocusLocationsOperations", ConfigurationName="FocusProcessesService.ILocationsOperationsService")]
    public interface ILocationsOperationsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusLocationsOperations/ILocationsOperationsService/AddLocationToObserved" +
            "LocationsList", ReplyAction="http://FocusLocationsOperations/ILocationsOperationsService/AddLocationToObserved" +
            "LocationsListResponse")]
        void AddLocationToObservedLocationsList(string path, string fileName, Focus.Core.LocationsHelpers.WatchedLocationActionType actionType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusLocationsOperations/ILocationsOperationsService/AddLocationToObserved" +
            "LocationsList", ReplyAction="http://FocusLocationsOperations/ILocationsOperationsService/AddLocationToObserved" +
            "LocationsListResponse")]
        System.Threading.Tasks.Task AddLocationToObservedLocationsListAsync(string path, string fileName, Focus.Core.LocationsHelpers.WatchedLocationActionType actionType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusLocationsOperations/ILocationsOperationsService/RemoveLocationFromObs" +
            "ervedLocationsList", ReplyAction="http://FocusLocationsOperations/ILocationsOperationsService/RemoveLocationFromObs" +
            "ervedLocationsListResponse")]
        void RemoveLocationFromObservedLocationsList(string path, string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusLocationsOperations/ILocationsOperationsService/RemoveLocationFromObs" +
            "ervedLocationsList", ReplyAction="http://FocusLocationsOperations/ILocationsOperationsService/RemoveLocationFromObs" +
            "ervedLocationsListResponse")]
        System.Threading.Tasks.Task RemoveLocationFromObservedLocationsListAsync(string path, string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusLocationsOperations/ILocationsOperationsService/GetAllWatchedLocation" +
            "s", ReplyAction="http://FocusLocationsOperations/ILocationsOperationsService/GetAllWatchedLocation" +
            "sResponse")]
        Focus.FocusProcessesService.WatchedLocationDto[] GetAllWatchedLocations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://FocusLocationsOperations/ILocationsOperationsService/GetAllWatchedLocation" +
            "s", ReplyAction="http://FocusLocationsOperations/ILocationsOperationsService/GetAllWatchedLocation" +
            "sResponse")]
        System.Threading.Tasks.Task<Focus.FocusProcessesService.WatchedLocationDto[]> GetAllWatchedLocationsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILocationsOperationsServiceChannel : Focus.FocusProcessesService.ILocationsOperationsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LocationsOperationsServiceClient : System.ServiceModel.ClientBase<Focus.FocusProcessesService.ILocationsOperationsService>, Focus.FocusProcessesService.ILocationsOperationsService {
        
        public LocationsOperationsServiceClient() {
        }
        
        public LocationsOperationsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LocationsOperationsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LocationsOperationsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LocationsOperationsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddLocationToObservedLocationsList(string path, string fileName, Focus.Core.LocationsHelpers.WatchedLocationActionType actionType) {
            base.Channel.AddLocationToObservedLocationsList(path, fileName, actionType);
        }
        
        public System.Threading.Tasks.Task AddLocationToObservedLocationsListAsync(string path, string fileName, Focus.Core.LocationsHelpers.WatchedLocationActionType actionType) {
            return base.Channel.AddLocationToObservedLocationsListAsync(path, fileName, actionType);
        }
        
        public void RemoveLocationFromObservedLocationsList(string path, string fileName) {
            base.Channel.RemoveLocationFromObservedLocationsList(path, fileName);
        }
        
        public System.Threading.Tasks.Task RemoveLocationFromObservedLocationsListAsync(string path, string fileName) {
            return base.Channel.RemoveLocationFromObservedLocationsListAsync(path, fileName);
        }
        
        public Focus.FocusProcessesService.WatchedLocationDto[] GetAllWatchedLocations() {
            return base.Channel.GetAllWatchedLocations();
        }
        
        public System.Threading.Tasks.Task<Focus.FocusProcessesService.WatchedLocationDto[]> GetAllWatchedLocationsAsync() {
            return base.Channel.GetAllWatchedLocationsAsync();
        }
    }
}
