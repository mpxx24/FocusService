using System;
using System.Windows;

namespace Focus.Views {
    /// <summary>
    ///     Interaction logic for AllowedTimeDialog.xaml
    /// </summary>
    public partial class AllowedTimeDialog : Window {
        public AllowedTimeDialog() {
            this.InitializeComponent();
        }

        public TimeSpan AllowedTimePerDay { get; private set; }

        private void OKButton_Click(object sender, RoutedEventArgs e) {
            if (!TimeSpan.TryParse(this.TimeAllowedTextBox.Text, out var result)) {
                MessageBox.Show("Incorrect time set!");
            }
            else {
                this.AllowedTimePerDay = result;
                this.DialogResult = true;
            }
        }
    }
}