using System;
using System.Collections;
using System.Linq;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace ApplManga {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
            //dataGrid.AutoGenerateColumns = true;

            //dataGrid.ItemsSource = Environment.GetEnvironmentVariables()
            //    .Cast<DictionaryEntry>().ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
