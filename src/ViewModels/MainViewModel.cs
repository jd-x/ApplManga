using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ApplManga.Utils.Base;
using ApplManga.Utils.Extensions;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
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

        private Dispatcher _dispatcher;

        /// <summary>
        /// <see cref="ICommand"/> for switching between different Views
        /// </summary>
        private ICommand _changeViewCommand;
        public ICommand ChangeViewCommand {
            get {
                if (_changeViewCommand == null) {
                    _changeViewCommand = new RelayCommand(p => ChangeViewModel((IPageViewModel)p), p => p is IPageViewModel);
                }

                return _changeViewCommand;
            }
        }

        private IPageViewModel _currentViewModel;
        public IPageViewModel CurrentViewModel {
            get { return _currentViewModel; }
            set {
                if (_currentViewModel != value) {
                    _currentViewModel = value;
                    RaisePropertyChanged("CurrentViewModel");
                }
            }
        }

        /// <summary>
        /// Holds the collection of ViewModels used to display the tabs in the main window
        /// </summary>
        private List<IPageViewModel> _pageViewModels;
        public List<IPageViewModel> PageViewModels {
            get {
                if (_pageViewModels == null) {
                    _pageViewModels = new List<IPageViewModel>();
                }

                return _pageViewModels;
            }
        }

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

        // Chrome button commands
        public ICommand MinimizeCommand { get; private set; }
        public ICommand MaximizeCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        /// <summary>
        /// Switches to the specified ViewModel
        /// </summary>
        /// <param name="viewModel"></param>
        private void ChangeViewModel(IPageViewModel viewModel) {
            if (!PageViewModels.Contains(viewModel)) {
                PageViewModels.Add(viewModel);
            }

            CurrentViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        public MainViewModel(Dispatcher dispatcher, Window window) {
            _mainWindow = window;
            _dispatcher = dispatcher;

            // Check if window is resized then fire all events affected by it
            _mainWindow.StateChanged += (sender, e) => {
                RaisePropertyChanged("ResizeBorderThickness");
            };

            // Commands for Chrome buttons
            MinimizeCommand = new RelayCommand(p => _mainWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(p => _mainWindow.WindowState ^= WindowState.Maximized);
            ExitCommand = new RelayCommand(p => _mainWindow.Close());

            // Fix for window resize issue
            var windowResizer = new WindowResizer(_mainWindow);

            PageViewModels.Add(new DownloadsViewModel("\xE896"));
            PageViewModels.Add(new BrowseMainViewModel("\xE82D"));
            PageViewModels.Add(new FavoritesViewModel("\xEB52"));
            PageViewModels.Add(new FoldersViewModel("\xED43"));

            SelectedTabIndex = 0;
        }
    }
}
