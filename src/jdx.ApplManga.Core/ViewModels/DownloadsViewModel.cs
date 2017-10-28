namespace jdx.ApplManga.Core.ViewModels {
    public class DownloadsViewModel : BaseViewModel {
        public string Name {
            get { return "Downloads"; }
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

        public DownloadsViewModel(string tabIcon) {
            TabCaption = Name;
            TabIcon = tabIcon;
        }

        public DownloadsViewModel() {

        }
    }
}
