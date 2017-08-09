using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
        private Dispatcher _dispatcher;

        private ObservableCollection<ViewModelBase> _tabItems = new ObservableCollection<ViewModelBase>();
        public ObservableCollection<ViewModelBase> TabItemCollection { get { return _tabItems; } }

        public MainViewModel(Dispatcher dispatcher) {
            this._dispatcher = dispatcher;

            TabItemCollection.Add(new DownloadsViewModel("DOWNLOADS", "DesktopDownload"));
            TabItemCollection.Add(new BrowseViewModel("BROWSE MANGA", "Book"));
            TabItemCollection.Add(new FavoritesViewModel("FAVORITES", "Star"));
            TabItemCollection.Add(new FoldersViewModel("MANAGE FOLDERS", "FileDirectory"));

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
