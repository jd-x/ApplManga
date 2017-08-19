using System.Windows;
using System.Windows.Input;
using ApplManga.ViewModels;

namespace ApplManga {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            // Custom window wrapper bindings
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));

            DataContext = new MainViewModel(this.Dispatcher);
            InitializeComponent();

            // Enables dragging for borderless forms
            //MouseLeftButtonDown += delegate { DragMove(); };
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnMinimizeWindow(object sender, ExecutedRoutedEventArgs e) {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnMaximizeWindow(object sender, ExecutedRoutedEventArgs e) {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnRestoreWindow(object sender, ExecutedRoutedEventArgs e) {
            SystemCommands.RestoreWindow(this);
        }

        private void OnCloseWindow(object sender, ExecutedRoutedEventArgs e) {
            SystemCommands.CloseWindow(this);
        }
    }
}
