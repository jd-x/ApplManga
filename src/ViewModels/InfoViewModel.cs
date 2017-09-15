namespace ApplManga.ViewModels {
    public class InfoViewModel : ViewModelBase, IPageViewModel {
        public string Name {
            get { return "INFO"; }
        }

        private string _selectedTitle;
        public string SelectedTitle {
            get { return _selectedTitle; }
            set { _selectedTitle = value; }
        }

        public InfoViewModel() {
        }
    }
}
