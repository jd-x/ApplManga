using jdx.ApplManga.Core.ViewModels;

namespace jdx.ApplManga.ViewModels {
    public class InfoViewModel : ViewModelBase, IPageViewModel {
        public string Name {
            get { return "INFO"; }
        }

        private string _selectedTitle;
        public string SelectedTitle {
            get { return _selectedTitle; }
            set { _selectedTitle = value; }
        }

        private string _selectedAuthor;
        public string SelectedAuthor {
            get { return _selectedAuthor; }
            set { _selectedAuthor = value; }
        }

        private string _selectedImage;
        public string SelectedImage {
            get { return _selectedImage; }
            set { _selectedImage = value; }
        }

        public InfoViewModel() {
        }
    }
}
