using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using jdx.ApplManga.Core.ViewModels;
using jdx.ApplManga.Utils.Base;
using jdx.ApplManga.Utils.Extensions;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using jdx.ApplManga.Views;
using jdx.ApplManga.Core.IOC;
using jdx.ApplManga.Core.Models;
using System.Threading.Tasks;

namespace jdx.ApplManga.ViewModels {
    public class MainViewModel : BaseViewModel {
        // TODO: Move window logic to another file
        #region Public properties

        private Window _mainWindow;

        public double MinimumWindowWidth { get; set; } = Constants.WindowMinimumWidth;
        public double MinimumWindowHeight { get; set; } = Constants.WindowMinimumHeight;

        /// <summary>
        /// Size of the window title bar area
        /// </summary>
        public int TitleBarHeight { get; set; } = (int)Constants.WindowTitleBarHeight;

        /// <summary>
        /// Resize border size around the window
        /// </summary>
        public int ResizeBorder { get; set; } = (int)Constants.WindowResizeBorderSize;

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder); } }

        /// <summary>
        /// Opacity for enabling/disabling window blur
        /// </summary>
        public double WindowOpacity { get; set; } = Constants.WindowOpacity;

        private Dispatcher _dispatcher;

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
        // Chrome button commands
        public ICommand MinimizeCommand { get; private set; }
        public ICommand MaximizeCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

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
        /// <param name="dispatcher"></param>
        /// <param name="window"></param>
        public MainViewModel(Dispatcher dispatcher, Window window) {
            _mainWindow = window;
            _dispatcher = dispatcher;

            // Check if window is resized then fire all events affected by it
            _mainWindow.StateChanged += (sender, e) => {
                RaisePropertyChanged(nameof(ResizeBorderThickness));
            };

            // Commands for Chrome buttons
            MinimizeCommand = new RelayCommand(() => _mainWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _mainWindow.WindowState ^= WindowState.Maximized);
            ExitCommand = new RelayCommand(() => _mainWindow.Close());

            // Commands for the side menu
            SwitchToDownloadsCommand = new RelayCommand(async () => await SwitchToDownloadsAsync());
            SwitchToBrowseCommand = new RelayCommand(async () => await SwitchToBrowseAsync());
            SwitchToFavoritesCommand = new RelayCommand(async () => await SwitchToFavoritesAsync());
            SwitchToFoldersCommand = new RelayCommand(async () => await SwitchToFoldersAsync());

            // Fix for window resize issue
            var windowResizer = new WindowResizer(_mainWindow);
        }
    }
}
