using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Focus.Core.LocationsHelpers;
using MessageBox = System.Windows.MessageBox;

namespace Focus.Views {
    /// <summary>
    ///     Interaction logic for WatchedLocationDialog.xaml
    /// </summary>
    public partial class WatchedLocationDialog : Window {
        public WatchedLocationDialog() {
            this.InitializeComponent();
            this.ActionsCombobox.Items.Add(WatchedLocationActionType.Delete);
        }

        public string WatchedLocation { get; private set; }

        public string FileName { get; private set; }

        public WatchedLocationActionType Action { get; private set; }

        private void OKButton_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(this.WatchedLocation)) {
                MessageBox.Show("You need to specify the watched location!");
                return;
            }
            if (string.IsNullOrEmpty(this.FileNameTextBox.Text)) {
                MessageBox.Show("You need to specify the file name!");
                return;
            }

            if (this.Action == WatchedLocationActionType.Nothing) {
                MessageBox.Show("You need to specify the watched location!");
                return;
            }

            this.FileName = this.FileNameTextBox.Text;
            this.DialogResult = true;
        }

        private void ChoosePathButon_Click(object sender, RoutedEventArgs e) {
            using (var dialog = new FolderBrowserDialog()) {
                dialog.ShowDialog();
                this.WatchedLocation = dialog.SelectedPath;
                this.LocationTextBox.Text = dialog.SelectedPath;
            }
        }

        private void ActionsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            this.Action = (WatchedLocationActionType) this.ActionsCombobox.SelectedItem;
        }
    }
}