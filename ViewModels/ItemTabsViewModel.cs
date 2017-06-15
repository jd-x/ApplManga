using System.Collections.Generic;

namespace ApplManga.ViewModels {
    public class ItemTabsViewModel : ViewModelBase {
        public MainViewModel MainViewModel { get; private set; }

        public ItemTabsViewModel(MainViewModel mainViewModel, string tabCaption, string tabIcon) {
            this.MainViewModel = mainViewModel;
            this.TabCaption = tabCaption;
            this.TabIcon = tabIcon;
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }
    }

    public interface IItemTabsViewModel {
        string TabName { get; set; }
    }
}
