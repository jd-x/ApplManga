using System.Collections.Generic;

namespace ApplManga.ViewModels {
    public class ItemTabsViewModel : ViewModelBase {
        public MainViewModel MainViewModel { get; private set; }

        public ItemTabsViewModel(MainViewModel mainViewModel, string tabCaption) {
            this.MainViewModel = mainViewModel;
            this.TabCaption = tabCaption;
        }

        public string TabCaption { get; private set; }
    }

    public interface IItemTabsViewModel {
        string TabName { get; set; }
    }
}
