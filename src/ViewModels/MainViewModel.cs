using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ApplManga.Utils.Base;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
        private Window _mainWindow;

        private int _borderMargin;
        public int BorderMargin {
            get {
                return _mainWindow.WindowState == WindowState.Maximized ? Constants.WindowBorderMargin * 2 : _borderMargin;
            }
            set {
                _borderMargin = value;
            }
        }

        private Dispatcher _dispatcher;

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

        private List<IPageViewModel> _pageViewModels;
        public List<IPageViewModel> PageViewModels {
            get {
                if (_pageViewModels == null) {
                    _pageViewModels = new List<IPageViewModel>();
                }

                return _pageViewModels;
            }
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex {
            get { return _selectedTabIndex; }
            set {
                if (value != _selectedTabIndex) {
                    _selectedTabIndex = value;
                }
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel) {
            if (!PageViewModels.Contains(viewModel)) {
                PageViewModels.Add(viewModel);
            }

            CurrentViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        public MainViewModel(Dispatcher dispatcher, Window window) {
            _mainWindow = window;
            _dispatcher = dispatcher;

            _borderMargin = Constants.WindowBorderMargin;

            // Check if window is resized
            _mainWindow.StateChanged += (sender, e) => {
                RaisePropertyChanged("BorderMargin");
            };

            PageViewModels.Add(new DownloadsViewModel("\xE896"));
            PageViewModels.Add(new BrowseMainViewModel("\xE82D"));
            PageViewModels.Add(new FavoritesViewModel("\xEB52"));
            PageViewModels.Add(new FoldersViewModel("\xED43"));

            SelectedTabIndex = 0;
        }
    }
}
