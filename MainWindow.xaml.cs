using System;
using System.Collections;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using MahApps.Metro.Controls;
using ApplManga.ViewModels;

namespace ApplManga {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            //dataGrid.AutoGenerateColumns = true;

            //dataGrid.ItemsSource = Environment.GetEnvironmentVariables()
            //    .Cast<DictionaryEntry>().ToList();
            var mainViewModel = new MainViewModel();
            mainViewModel.Tabs.Add(new ItemViewModel("Home"));
            mainViewModel.Tabs.Add(new ItemViewModel("Manga List"));

            this.DataContext = mainViewModel;
            InitializeComponent();
        }
    }
}
