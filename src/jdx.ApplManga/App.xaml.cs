using System.Windows;

namespace jdx.ApplManga {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            // Code snippet adapted from: http://www.wpf-tutorial.com/wpf-application/handling-exceptions/
            // TODO: Exception handling
            MessageBox.Show("An unhandled error just occured: " + e.Exception.Message, "Oops", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
