using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
        private Dispatcher _dispatcher;

        public MainViewModel(Dispatcher dispatcher) {
            this._dispatcher = dispatcher;

            TabItemsCollection.Add(new DownloadsViewModel("DOWNLOADS", "DesktopDownload"));
            TabItemsCollection.Add(new BrowseViewModel("BROWSE MANGA", "Book"));
            TabItemsCollection.Add(new FavoritesViewModel("FAVORITES", "Star"));
            TabItemsCollection.Add(new FoldersViewModel("MANAGE FOLDERS", "FileDirectory"));

            SelectedTabIndex = 0;
        }

        private ObservableCollection<ViewModelBase> _tabItems = new ObservableCollection<ViewModelBase>();
        public ObservableCollection<ViewModelBase> TabItemsCollection { get { return _tabItems;  } }

        private int _selectedTabIndex;
        public int SelectedTabIndex {
            get { return _selectedTabIndex; }
            set {
                if (value != _selectedTabIndex) {
                    _selectedTabIndex = value;
                }
            }
        }
    }
}
