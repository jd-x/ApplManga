using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using jdx.ApplManga.Core.ViewModels;

namespace jdx.ApplManga.ViewModels {
    public class BrowseMainViewModel : ViewModelBase, IPageViewModel {
        public string Name {
            get { return "Browse"; }
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

        private void BrowseViewModel_OnSelectionChange(string title, string author, string imagePath) {
            InfoViewModel infoViewModel = (InfoViewModel)BrowseViewModels[1];
            infoViewModel.SelectedTitle = title;
            infoViewModel.SelectedAuthor = author;
            infoViewModel.SelectedImage = imagePath;
        }

        public BrowseMainViewModel(string tabIcon) {
            TabCaption = Name;
            TabIcon = tabIcon;

            BrowseViewModel browseViewModel = new BrowseViewModel();
            InfoViewModel infoViewModel = new InfoViewModel();

            browseViewModel.OnSelectionChange += BrowseViewModel_OnSelectionChange;

            BrowseViewModels.Add(browseViewModel);
            BrowseViewModels.Add(infoViewModel);

            CurrentViewModel = BrowseViewModels[0];
        }
    }
}
