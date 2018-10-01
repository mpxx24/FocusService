using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using Focus.FocusService;

namespace Focus {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        private readonly TimeSpan testTimeSpan = new TimeSpan(0, 30, 0);

        private ProcessesOperationsServiceClient client;

        public MainWindow() {
            this.InitializeComponent();
            this.client = new ProcessesOperationsServiceClient();
        }

        private void On_Window_Loaded(object sender, RoutedEventArgs e) {
            this.FullyRefreshProcessesListView();
        }

        private IEnumerable<ProcessModel> GetProcessList() {
            var processes = Process.GetProcesses(); //this.client.GetAllRunningProcesses();

            return new List<ProcessModel>(processes.Select(x => new ProcessModel {
                Id = x.Id,
                Name = x.ProcessName,
                TimePerDay = this.testTimeSpan.ToString("g")
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
                if (this.client.InnerChannel.State != CommunicationState.Faulted) {
                    this.client.KillProcess(selectedItem.Name);
                }
                else {
                    this.client = new ProcessesOperationsServiceClient();
                    this.KillProcessButton_OnClick(sender, e);
                }
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not kill selected process [{selectedItem.Name}]. Windows service 'FocusHostService' is most likely not running.");
            }
            finally {
                this.FullyRefreshProcessesListView();
            }
        }

        private void FullyRefreshProcessesListView() {
            this.ProcessesListView.ItemsSource = this.GetProcessList().OrderBy(x => x.Name);
        }

        private void AddTimeLimitButton_OnClick(object sender, RoutedEventArgs e) {
            var selectedItem = this.GetCurrentlySelectedItem();
            try {
                if (this.client.InnerChannel.State != CommunicationState.Faulted) {
                    this.client.AddProcessToObservedProcessesList(selectedItem.Name, this.testTimeSpan);
                }
                else {
                    this.client = new ProcessesOperationsServiceClient();
                    this.AddTimeLimitButton_OnClick(sender, e);
                }
            }
            catch (EndpointNotFoundException) {
                MessageBox.Show($"Could not add process [{selectedItem.Name}] to observed. Windows service 'FocusHostService' is most likely not running.");
            }
        }

        private void RemoveTimeLimitButton_OnClick(object sender, RoutedEventArgs e) {
            var selectedItem = this.GetCurrentlySelectedItem();
            try {
                if (this.client.InnerChannel.State != CommunicationState.Faulted) {
                    this.client.RemoveProcessFromObservedProcessesList(selectedItem.Name);
                }
                else {
                    this.client = new ProcessesOperationsServiceClient();
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
    }
}