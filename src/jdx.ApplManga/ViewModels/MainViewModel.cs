using System.Windows;
using System.Windows.Input;
using jdx.ApplManga.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Threading.Tasks;
using jdx.ApplManga.Core.IOC;
using jdx.ApplManga.Core.Models;

namespace jdx.ApplManga.ViewModels {
    public class MainViewModel : WindowViewModel {
        #region Public properties

        /// <summary>
        /// Holds the collection of ViewModels used to display the tabs in the main window
        /// </summary>
        //private List<IPageViewModel> _pageViewModels;
        //public List<IPageViewModel> PageViewModels => _pageViewModels ?? (_pageViewModels = new List<IPageViewModel>());
        public ObservableCollection<TabItem> PageViewModels { get; set; }

        /// <summary>
        /// Returns the currently selected TabControl index
        /// </summary>
        private int _selectedTabIndex;
        public int SelectedTabIndex {
            get { return _selectedTabIndex; }
            set {
                if (value != _selectedTabIndex) {
                    _selectedTabIndex = value;
                }
            }
        }

        #endregion

        #region Commands

        // Side menu commands
        public ICommand SwitchToDownloadsCommand { get; private set; }
        public ICommand SwitchToBrowseCommand { get; private set; }
        public ICommand SwitchToFavoritesCommand { get; private set; }
        public ICommand SwitchToFoldersCommand { get; private set; }

        #endregion

        #region Tasks

        public async Task SwitchToDownloadsAsync() {
            IoC.Get<AppViewModel>().SwitchToView(AppView.Downloads);
            System.Console.WriteLine("Switch to Downloads invoked");

            await Task.Delay(1);
        }

        public async Task SwitchToBrowseAsync() {
            IoC.Get<AppViewModel>().SwitchToView(AppView.Browse);
            System.Console.WriteLine("Switch to Browse invoked");

            await Task.Delay(1);
        }

        public async Task SwitchToFavoritesAsync() {
            IoC.Get<AppViewModel>().SwitchToView(AppView.Favorites);
            System.Console.WriteLine("Switch to Favorites invoked");

            await Task.Delay(1);
        }

        public async Task SwitchToFoldersAsync() {
            IoC.Get<AppViewModel>().SwitchToView(AppView.Folders);
            System.Console.WriteLine("Switch to Browse invoked");

            await Task.Delay(1);
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public MainViewModel(Window window) : base(window) {
            // Commands for the side menu
            SwitchToDownloadsCommand = new RelayCommand(async () => await SwitchToDownloadsAsync());
            SwitchToBrowseCommand = new RelayCommand(async () => await SwitchToBrowseAsync());
            SwitchToFavoritesCommand = new RelayCommand(async () => await SwitchToFavoritesAsync());
            SwitchToFoldersCommand = new RelayCommand(async () => await SwitchToFoldersAsync());
        }
    }
}
