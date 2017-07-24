using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
        private Dispatcher _dispatcher;

        private ObservableCollection<ViewModelBase> _tabItems = new ObservableCollection<ViewModelBase>();
        public ObservableCollection<ViewModelBase> TabItemCollection { get { return _tabItems; } }

        public MainViewModel(Dispatcher dispatcher) {
            this._dispatcher = dispatcher;

            TabItemCollection.Add(new DownloadsViewModel("Downloads", "DesktopDownload"));
            TabItemCollection.Add(new BrowseViewModel("Browse Manga", "Book"));
            TabItemCollection.Add(new FavoritesViewModel("Favorites", "Star"));
            TabItemCollection.Add(new FoldersViewModel("Manage Folders", "FileDirectory"));

            SelectedTabIndex = 0;
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
    }
}
