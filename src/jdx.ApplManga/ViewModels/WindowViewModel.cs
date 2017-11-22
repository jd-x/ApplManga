using System.Windows;
using System.Windows.Input;
using jdx.ApplManga.Core.ViewModels;
using jdx.ApplManga.Utils.Base;
using jdx.ApplManga.Utils.Extensions;

namespace jdx.ApplManga.ViewModels {
    public class WindowViewModel : BaseViewModel {
        #region Private members

        private Window _window;

        #endregion

        #region Public properties

        public double MinimumWindowWidth { get; set; } = Constants.WindowMinimumWidth;

        public double MinimumWindowHeight { get; set; } = Constants.WindowMinimumHeight;

        /// <summary>
        /// Size of the window title bar area
        /// </summary>
        public int TitleBarHeight { get; set; } = (int)Constants.WindowTitleBarHeight;

        /// <summary>
        /// Resize border size around the window
        /// </summary>
        public int ResizeBorder { get; set; } = (int)Constants.WindowResizeBorderSize;

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder); } }

        /// <summary>
        /// Opacity for enabling/disabling window blur
        /// </summary>
        public double WindowOpacity { get; set; } = Constants.WindowOpacity;

        /// <summary>
        /// Dimmed overlay visibility (true when there's a dialog window
        /// over the application or when the main window is not in focus)
        /// </summary>
        public bool DimOverlayVisible { get; set; }

        #endregion

        #region Commands

        // Chrome button commands
        public ICommand MinimizeCommand { get; private set; }
        public ICommand MaximizeCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        #endregion
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window) {
            _window = window;

            // Check if window is resized then fire all events affected by it
            _window.StateChanged += (sender, e) => {
                RaisePropertyChanged(nameof(ResizeBorderThickness));
            };

            // Commands for Chrome buttons
            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            ExitCommand = new RelayCommand(() => _window.Close());

            // Fix for window resize issue
            var windowResizer = new WindowResizer(_window);
        }
    }
}
