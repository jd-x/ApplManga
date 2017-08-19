using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ApplManga.ViewModels {
    public class BrowseMainViewModel : ViewModelBase, IPageViewModel {
        public string Name {
            get { return "BROWSE MANGA"; }
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

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

        private List<IPageViewModel> _browseViewModels;
        public List<IPageViewModel> BrowseViewModels {
            get {
                if (_browseViewModels == null) {
                    _browseViewModels = new List<IPageViewModel>();
                }

                return _browseViewModels;
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
            if (!BrowseViewModels.Contains(viewModel)) {
                BrowseViewModels.Add(viewModel);
            }

            CurrentViewModel = BrowseViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        public ICommand DisplayInfoView {
            get {
                return new RelayCommand(vm => ChangeViewModel(BrowseViewModels[1]));
            }
        }

        public ICommand DisplayBrowseView {
            get {
                return new RelayCommand(vm => ChangeViewModel(BrowseViewModels[0]));
            }
        }

        public BrowseMainViewModel(string tabIcon) {
            TabCaption = Name;
            TabIcon = tabIcon;

            BrowseViewModels.Add(new BrowseViewModel());
            BrowseViewModels.Add(new InfoViewModel());

            CurrentViewModel = BrowseViewModels[0];
        }
    }
}
