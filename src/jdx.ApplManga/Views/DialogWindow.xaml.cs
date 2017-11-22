using System.Windows;
using jdx.ApplManga.ViewModels;

namespace jdx.ApplManga {
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window {
        #region Public properties

        private DialogWindowViewModel _dialogViewModel;
        public DialogWindowViewModel DialogViewModel {
            get => _dialogViewModel;
            set {
                if(_dialogViewModel != value)
                    _dialogViewModel = value;

                // Set the DataContext every time the ViewModel changes
                DataContext = _dialogViewModel;
            }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public DialogWindow() {
            InitializeComponent();
        }
    }
}
