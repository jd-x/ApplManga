using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
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

        public MainViewModel(Dispatcher dispatcher) {
            _dispatcher = dispatcher;

            PageViewModels.Add(new DownloadsViewModel("DesktopDownload"));
            PageViewModels.Add(new BrowseMainViewModel("Book"));
            //PageViewModels.Add(new BrowseViewModel("Book"));
            //PageViewModels.Add(new InfoViewModel("Info"));
            PageViewModels.Add(new FavoritesViewModel("Heart"));
            PageViewModels.Add(new FoldersViewModel("FileDirectory"));

            SelectedTabIndex = 0;
        }
    }
}
