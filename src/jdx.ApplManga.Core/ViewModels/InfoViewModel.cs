using jdx.ApplManga.Core.IOC;
using jdx.ApplManga.Core.Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace jdx.ApplManga.Core.ViewModels {
    public class InfoViewModel : BaseViewModel {
        public string Name {
            get { return "INFO"; }
        }

        private string _selectedTitle;
        public string SelectedTitle {
            get => _selectedTitle;
            set => _selectedTitle = value;
        }

        private string _selectedAuthor;
        public string SelectedAuthor {
            get => _selectedAuthor;
            set => _selectedAuthor = value;
        }

        private string _selectedImage;
        public string SelectedImage {
            get => _selectedImage;
            set => _selectedImage = value;
        }

        public ICommand SwitchToBrowseCommand { get; set; }

        public async Task SwitchToBrowseAsync() {
            IoC.Get<AppViewModel>().SwitchToView(AppView.Browse);

            await Task.Delay(1);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public InfoViewModel() {
            SwitchToBrowseCommand = new RelayCommand(async () => await SwitchToBrowseAsync());
        }
    }
}
