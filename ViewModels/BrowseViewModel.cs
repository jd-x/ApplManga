namespace ApplManga.ViewModels {
    public class BrowseViewModel : ViewModelBase {
        public BrowseViewModel(string tabCaption, string tabIcon) {
            this.TabCaption = tabCaption;
            this.TabIcon = tabIcon;
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }
    }
}
