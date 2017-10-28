using System;
using System.Windows;
using jdx.ApplManga.Core.IOC;
using jdx.ApplManga.IOC;

namespace jdx.ApplManga {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        /// <summary>
        /// Generic error handler for catching unhandled exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            // Code snippet adapted from: http://www.wpf-tutorial.com/wpf-application/handling-exceptions/
            // TODO: Exception handling
            MessageBox.Show("An unhandled error just occurred: " + e.Exception.Message, "Oops", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Configure the app for use
        /// </summary>
        private void ApplicationSetup() {
            IoC.Setup();

            // Bind the UI Manager
            IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());
        }

        /// <summary>
        /// Load IoC immediately
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            ApplicationSetup();

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
