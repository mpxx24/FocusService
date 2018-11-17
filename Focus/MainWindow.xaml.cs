using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using Focus.Core.LocationsHelpers;
using Focus.FocusProcessesService;
using Focus.Helpers;
using Focus.Views;

namespace Focus {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        private readonly LocationsOperationsServiceClient locationsOperationsClient;
        private ProcessesOperationsServiceClient processesOperationsClient;

        public MainWindow() {
            this.InitializeComponent();
            this.processesOperationsClient = new ProcessesOperationsServiceClient();
            this.locationsOperationsClient = new LocationsOperationsServiceClient();
        }

        private void On_Window_Loaded(object sender, RoutedEventArgs e) {
            this.FullyRefreshProcessesListViews();
        }

        private IEnumerable<ProcessModel> GetProcessList() {
            var processes = Process.GetProcesses();

            return new List<ProcessModel>(processes.Select(x => new ProcessModel {
                Id = x.Id,
                Name = x.ProcessName
            }));
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e) {
            this.ProcessesListView.ItemsSource = new List<ProcessModel>();

            var listViewSource = string.IsNullOrEmpty(this.SearchTextBox.Text)
                ? this.GetProcessList()
                : this.GetProcessList()
                      .Where(x => x.Name.StartsWith(this.SearchTextBox.Text, StringComparison.InvariantCultureIgnoreCase));

            this.ProcessesListView.ItemsSource = listViewSource.OrderBy(x => x.Name);
        }

        private void KillProcessButton_OnClick(object sender, RoutedEventArgs e) {
            var selectedItem = (ProcessModel) this.ProcessesListView.SelectedItem;
            try {
                if (this.processesOperationsClient.InnerChannel.State != CommunicationState.Faulted) {
                    this.processesOperationsClient.KillProcess(selectedItem.Name);
                }
                else {
                    this.processesOperationsClient = new ProcessesOperationsServiceClient();
                    this.KillProcessButton_OnClick(sender, e);
                }
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not kill selected process [{selectedItem.Name}]. Windows service 'FocusHostService' is most likely not running.");
            }
            finally {
                this.FullyRefreshProcessesListViews();
            }
        }

        private void FullyRefreshProcessesListViews() {
            this.RefreshProcessesListView();
            this.RefreshWatchedProcessesListView();
            this.RefreshWatchedLocationsListView();
        }

        private void RefreshProcessesListView() {
            this.ProcessesListView.ItemsSource = this.GetProcessList().OrderBy(x => x.Name);
        }

        private void RefreshWatchedProcessesListView() {
            this.ProcessesWatchedListView.ItemsSource = this.GetWatchedProcesses().OrderBy(x => x.Name);
        }

        private void AddTimeLimitButton_OnClick(object sender, RoutedEventArgs e) {
            var timeSpanToSet = new TimeSpan();
            var allowedTimeDialog = new AllowedTimeDialog();

            if (allowedTimeDialog.ShowDialog() == true) {
                timeSpanToSet = allowedTimeDialog.AllowedTimePerDay;
            }

            var selectedItem = this.GetCurrentlySelectedItem();
            try {
                if (this.processesOperationsClient.InnerChannel.State != CommunicationState.Faulted) {
                    this.processesOperationsClient.AddProcessToObservedProcessesList(selectedItem.Name, timeSpanToSet);
                    this.RefreshWatchedProcessesListView();
                }
                else {
                    this.processesOperationsClient = new ProcessesOperationsServiceClient();
                    this.AddTimeLimitButton_OnClick(sender, e);
                }
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not add process [{selectedItem.Name}] to observed. Windows service 'FocusHostService' is most likely not running.");
            }
        }

        private void ChangeTimeLimitButton_OnClick(object sender, RoutedEventArgs e) {
            var timeSpanToSet = new TimeSpan();
            var allowedTimeDialog = new AllowedTimeDialog();

            if (allowedTimeDialog.ShowDialog() == true) {
                timeSpanToSet = allowedTimeDialog.AllowedTimePerDay;
            }

            var selectedItem = this.GetCurrentlySelectedWatchedItem();
            try {
                if (this.processesOperationsClient.InnerChannel.State != CommunicationState.Faulted) {
                    this.processesOperationsClient.UpdateProcessInObservedProcessesList(selectedItem.Name, timeSpanToSet);
                    this.RefreshWatchedProcessesListView();
                }
                else {
                    this.processesOperationsClient = new ProcessesOperationsServiceClient();
                    this.AddTimeLimitButton_OnClick(sender, e);
                }
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not add process [{selectedItem.Name}] to observed. Windows service 'FocusHostService' is most likely not running.");
            }
        }

        private void RemoveTimeLimitButton_OnClick(object sender, RoutedEventArgs e) {
            var selectedItem = this.GetCurrentlySelectedWatchedItem();
            try {
                if (this.processesOperationsClient.InnerChannel.State != CommunicationState.Faulted) {
                    this.processesOperationsClient.RemoveProcessFromObservedProcessesList(selectedItem.Name);
                    this.RefreshWatchedProcessesListView();
                }
                else {
                    this.processesOperationsClient = new ProcessesOperationsServiceClient();
                    this.RemoveTimeLimitButton_OnClick(sender, e);
                }
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not remove process [{selectedItem.Name}] from observed. Windows service 'FocusHostService' is most likely not running.");
            }
        }

        private ProcessModel GetCurrentlySelectedItem() {
            return (ProcessModel) this.ProcessesListView.SelectedItem;
        }

        private WatchedProcessModel GetCurrentlySelectedWatchedItem() {
            return (WatchedProcessModel) this.ProcessesWatchedListView.SelectedItem;
        }

        private IEnumerable<WatchedProcessModel> GetWatchedProcesses() {
            try {
                if (this.processesOperationsClient.InnerChannel.State != CommunicationState.Faulted) {
                    var processes = this.processesOperationsClient.GetAllWatchedProcesses();

                    return processes.Select(x => new WatchedProcessModel {
                        Name = x.Name,
                        TimePerDay = x.TimeAllowedPerDay.ToString(),
                        TimeLeftToday = x.TimeLeft.ToString(),
                        IsProcessRunning = x.IsCurrentlyRunning ? YesNoHelper.Yes : YesNoHelper.No
                    });
                }

                this.processesOperationsClient = new ProcessesOperationsServiceClient();
                return this.GetWatchedProcesses();
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not get watched processes list. Windows service 'FocusHostService' is most likely not running.");
            }
            return new List<WatchedProcessModel>();
        }

        private void RefreshWatchedProcessesListButton_OnClick(object sender, RoutedEventArgs e) {
            this.RefreshWatchedProcessesListView();
        }

        private void AddWatchedLocationButton_OnClick(object sender, RoutedEventArgs e) {
            string location = string.Empty, fileName = string.Empty;
            var action = WatchedLocationActionType.Nothing;
            var allowedTimeDialog = new WatchedLocationDialog();

            if (allowedTimeDialog.ShowDialog() == true) {
                location = allowedTimeDialog.WatchedLocation;
                fileName = allowedTimeDialog.FileName;
                action = allowedTimeDialog.Action;
            }

            try {
                if (this.locationsOperationsClient.InnerChannel.State != CommunicationState.Faulted) {
                    this.locationsOperationsClient.AddLocationToObservedLocationsList(location, fileName, action);
                    this.RefreshWatchedLocationsListView();
                }
                else {
                    this.processesOperationsClient = new ProcessesOperationsServiceClient();
                    this.AddWatchedLocationButton_OnClick(sender, e);
                }
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not add location [{location}] and file [{fileName}] to observed. Windows service 'FocusHostService' is most likely not running.");
            }
        }

        private void RefreshWatchedLocationsListView() {
            this.LocationsWatchedListView.ItemsSource = this.GetWatchedLocations();
        }

        private IEnumerable<WatchedLocationModel> GetWatchedLocations() {
            try {
                if (this.processesOperationsClient.InnerChannel.State != CommunicationState.Faulted) {
                    var processes = this.locationsOperationsClient.GetAllWatchedLocations();

                    return processes.Select(x => new WatchedLocationModel {
                        Location = x.LocationPath,
                        FileName = x.FileName,
                        ActionType = x.ActionType.ToString()
                    });
                }

                this.processesOperationsClient = new ProcessesOperationsServiceClient();
                return this.GetWatchedLocations();
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not get watched locations list. Windows service 'FocusHostService' is most likely not running.");
            }
            return new List<WatchedLocationModel>();
        }

        private void RemoveWatchedLocationButton_OnClick(object sender, RoutedEventArgs e) {
            this.LocationsWatchedListView.ItemsSource = this.GetWatchedLocations().OrderBy(x => x.Location);
        }
    }
}