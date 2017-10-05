namespace ApplManga.ViewModels {
    public class DownloadsViewModel : ViewModelBase, IPageViewModel {
        public string Name {
            get { return "Downloads"; }
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

        public DownloadsViewModel(string tabIcon) {
            TabCaption = Name;
            TabIcon = tabIcon;
        }
    }
}
