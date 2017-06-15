using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
        private Dispatcher _dispatcher;

        public MainViewModel(Dispatcher dispatcher) {
            this._dispatcher = dispatcher;
            this.ItemTabs = new ObservableCollection<ItemTabsViewModel>(
                new[] {
                    new ItemTabsViewModel(this, "DOWNLOADS", "DesktopDownload"),
                    new ItemTabsViewModel(this, "BROWSE MANGA", "Book"),
                    new ItemTabsViewModel(this, "FAVORITES", "Star")
                });
        }

        public ObservableCollection<ItemTabsViewModel> ItemTabs { get; set; }
    }
}
