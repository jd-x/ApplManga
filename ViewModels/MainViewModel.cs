using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ApplManga.ViewModels {
    public class MainViewModel : ViewModelBase {
        private ObservableCollection<ItemViewModel> tabs = new ObservableCollection<ItemViewModel>();

        public ObservableCollection<ItemViewModel> Tabs {
            get {
                return tabs;
            }
        }
    }
}
