namespace ApplManga.ViewModels {
    public class FavoritesViewModel : ViewModelBase {
        public FavoritesViewModel(string tabCaption, string tabIcon) {
            this.TabCaption = tabCaption;
            this.TabIcon = tabIcon;
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }
    }
}
