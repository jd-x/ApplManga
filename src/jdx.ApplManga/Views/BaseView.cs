using jdx.ApplManga.Core.IOC;
using jdx.ApplManga.Core.ViewModels;
using jdx.ApplManga.Utils.Animations;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace jdx.ApplManga.Views {
    /// <summary>
    /// Handles all the views for the application
    /// </summary>
    public class BaseView : UserControl {
        // Set animations for page load and unload
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.FadeIn;
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.FadeOut;

        // Set animation length
        public float SlideDuration { get; set; } = 0.3f;

        // Flag to indicate whether this page should be animated on load
        public bool ShouldAnimateOut { get; set; }

        /// <summary>
        /// Determine the animation to be played on page load
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync() {
            if (PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation) {
                case PageAnimation.SlideAndFadeInFromBottom:
                    await this.SlideAndFadeInFromBottomAsync(SlideDuration, animHeight: (int)Application.Current.MainWindow.Height);
                    break;
                case PageAnimation.FadeIn:
                    await this.FadeInAsync(SlideDuration);
                    break;
            }
        }

        /// <summary>
        /// Determine the animation to be played on page unload
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOutAsync() {
            if (PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation) {
                case PageAnimation.SlideAndFadeOutToBottom:
                    await this.SlideAndFadeOutToBottomAsync(SlideDuration, animHeight: (int)Application.Current.MainWindow.Height);
                    break;
                case PageAnimation.FadeOut:
                    await this.FadeOutAsync(SlideDuration);
                    break;
            }
        }

        /// <summary>
        /// Starting animating once the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BaseView_LoadedAsync(object sender, RoutedEventArgs e) {
            if (ShouldAnimateOut)
                await AnimateOutAsync();
            else
                await AnimateInAsync();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseView() {
            // Don't animate during design time
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // Hide page before starting onload animation
            if (PageLoadAnimation != PageAnimation.None)
                Visibility = Visibility.Collapsed;

            // Listen for page load
            Loaded += BaseView_LoadedAsync;
        }
    }

    /// <summary>
    /// A base page with ViewModel support
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public class BaseView<VM> : BaseView where VM : BaseViewModel, new() {
        private VM _viewModel;

        public VM ViewModel {
            get => _viewModel;
            set {
                if (_viewModel == value)
                    return;

                _viewModel = value;
                this.DataContext = _viewModel;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="viewModel">The specified ViewModel (if any)</param>
        public BaseView(VM viewModel = null) : base() {
            // Set if a specific ViewModel is provided
            if (viewModel != null)
                ViewModel = viewModel;

            // Else create a default one
            this.ViewModel = IoC.Get<VM>();
        }
    }
}
