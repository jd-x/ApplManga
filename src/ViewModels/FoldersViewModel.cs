namespace ApplManga.ViewModels {
    public class FoldersViewModel : ViewModelBase, IPageViewModel {
        public string Name {
            get { return "Folders"; }
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

        public FoldersViewModel(string tabIcon) {
            TabCaption = Name;
            TabIcon = tabIcon;
        }
    }
}
