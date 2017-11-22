using jdx.ApplManga.Core.ViewModels;
using jdx.ApplManga.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace jdx.ApplManga.Controls.DialogEx {
    /// <summary>
    /// Base class for any content used in a <see cref="DialogWindow"/>
    /// </summary>
    public class BaseDialogControl : UserControl {

        #region Private members

        private DialogWindow _dialogWindow;

        #endregion

        #region Public properties

        public double MinimumDialogWindowWidth { get; set; } = 300;

        public double MinimumDialogWindowHeight { get; set; } = 100;

        public string DialogTitle { get; set; }

        #endregion

        #region Commands
        
        /// <summary>
        /// Dismisses the currently opened dialog window
        /// </summary>
        public ICommand CloseDialogWindowCommand { get; private set; }

        #endregion

        /// <summary>
        /// Displays a message dialog instance
        /// </summary>
        /// <param name="viewModel">The ViewModel</param>
        /// <typeparam name="T">The ViewModel type for this control</typeparam>
        /// <returns></returns>
        public Task ShowDialog<T>(T viewModel) where T : BaseDialogViewModel {
            // Create a task to await the dialog closing
            var taskCompletionSource = new TaskCompletionSource<bool>();

            Application.Current.Dispatcher.Invoke(() => {
                try {
                    _dialogWindow.DialogViewModel.MinimumWindowWidth = MinimumDialogWindowWidth;
                    _dialogWindow.DialogViewModel.MinimumWindowHeight = MinimumDialogWindowHeight;
                    _dialogWindow.DialogViewModel.DialogTitle = string.IsNullOrEmpty(viewModel.DialogTitle) ? DialogTitle : viewModel.DialogTitle;
                    _dialogWindow.DialogViewModel.DialogContent = this;

                    DataContext = viewModel;

                    _dialogWindow.ShowDialog();
                }
                finally {
                    // Ensure that the caller always gets completed regardless of whether there's an error or not
                    taskCompletionSource.TrySetResult(true);
                }
            });

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseDialogControl() {
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                _dialogWindow = new DialogWindow();
                _dialogWindow.DialogViewModel = new DialogWindowViewModel(_dialogWindow);

                CloseDialogWindowCommand = new RelayCommand(() => _dialogWindow.Close());
            }
        }
    }
}
