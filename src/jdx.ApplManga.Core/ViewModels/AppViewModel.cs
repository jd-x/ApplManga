using jdx.ApplManga.Core.Models;

namespace jdx.ApplManga.Core.ViewModels {
    /// <summary>
    /// Main application ViewModel
    /// </summary>
    public class AppViewModel : BaseViewModel {
        private AppView _currentView = AppView.Browse;
        public AppView CurrentView {
            get { return _currentView; }
            set {
                if (_currentView == value)
                    return;

                _currentView = value;
                RaisePropertyChanged(nameof(CurrentView));
            }
        }

        public BaseViewModel CurrentViewModel { get; set; }

        /// <summary>
        /// Navigate to the selected view
        /// </summary>
        /// <param name="view">The View to switch to</param>
        /// <param name="viewModel">The ViewModel (if any) to set to the specified View</param>
        public void SwitchToView(AppView view, BaseViewModel viewModel = null) {
            // Set the ViewModel first
            CurrentViewModel = viewModel;

            // Then set the View afterwards
            CurrentView = view;
        }
    }
}
