using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using jdx.ApplManga.Converters;
using jdx.ApplManga.Core.IOC;
using jdx.ApplManga.Core.ViewModels;
using jdx.ApplManga.Views;
using jdx.ApplManga.Core.Models;

namespace jdx.ApplManga.Controls.PageHostEx {
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl {
        /// <summary>
        /// The current View to be displayed in the page host
        /// </summary>
        public AppView CurrentView {
            get => (AppView)GetValue(CurrentViewProperty);
            set => SetValue(CurrentViewProperty, value);
        }

        /// <summary>
        /// The current ViewModel to be used if explicitly set
        /// </summary>
        public BaseViewModel CurrentViewModel {
            get => (BaseViewModel)GetValue(CurrentViewModelProperty);
            set => SetValue(CurrentViewModelProperty, value);
        }

        /// <summary>
        /// Registers <see cref="CurrentView"/> as a dependency property
        /// </summary>
        public static readonly DependencyProperty CurrentViewProperty = DependencyProperty.Register(nameof(CurrentView), typeof(AppView), typeof(PageHost), new UIPropertyMetadata(default(AppView), null, CurrentViewPropertyChanged));

        /// <summary>
        /// Register <see cref="CurrentViewModel"/> as a dependency property
        /// </summary>
        public static readonly DependencyProperty CurrentViewModelProperty = DependencyProperty.Register(nameof(CurrentViewModel), typeof(BaseViewModel), typeof(PageHost), new UIPropertyMetadata());

        /// <summary>
        /// Called when <see cref="CurrentView"/> is changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object CurrentViewPropertyChanged(DependencyObject d, object value) {
            // Get the current values
            var currentView = d.GetValue(CurrentViewProperty);
            var currentViewModel = d.GetValue(CurrentViewModelProperty);

            var oldPageFrame = (d as PageHost).OldPage;
            var newPageFrame = (d as PageHost).NewPage;

            var oldPageContent = newPageFrame.Content;

            newPageFrame.Content = null;

            oldPageFrame.Content = oldPageContent;
            
            if (oldPageContent is BaseView oldPage) {
                oldPage.ShouldAnimateOut = true;

                Task.Delay((int)(oldPage.SlideDuration * 1000)).ContinueWith((t) => {
                    Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                });
            }

            newPageFrame.Content = new AppViewValueConverter().Convert(currentView, null, currentViewModel);

            return value;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PageHost() {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                NewPage.Content = (BaseView)new AppViewValueConverter().Convert(IoC.Get<AppViewModel>().CurrentView);
        }
    }
}
