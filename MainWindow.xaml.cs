using MahApps.Metro.Controls;
using ApplManga.ViewModels;

namespace ApplManga {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {

            this.DataContext = new MainViewModel(this.Dispatcher);
            InitializeComponent();
        }
    }
}
