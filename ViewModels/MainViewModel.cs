using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
        private Dispatcher _dispatcher;

        public MainViewModel(Dispatcher dispatcher) {
            this._dispatcher = dispatcher;
            this.ItemTabs = new ObservableCollection<ItemTabsViewModel>(
                new[] {
                    new ItemTabsViewModel(this, "Home"),
                    new ItemTabsViewModel(this, "Manga List"),
                    new ItemTabsViewModel(this, "Favorites")
                });
        }

        public ObservableCollection<ItemTabsViewModel> ItemTabs { get; set; }
    }
}
