namespace jdx.ApplManga.Core.ViewModels {
    public class FavoritesViewModel : BaseViewModel {
        public string Name {
            get { return "Favorites"; }
        }

        public string TabCaption { get; private set; }

        public string TabIcon { get; private set; }

        public FavoritesViewModel(string tabIcon) {
            TabCaption = Name;
            TabIcon = tabIcon;
        }

        public FavoritesViewModel() {

        }
    }
}
