using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace jdx.ApplManga.Controls.CommandControlEx {
    public enum SelectMode {
        Single,
        Multiple,
    }

    public partial class CommandControl : UserControl {
        public static DependencyProperty SelectModeProperty = DependencyProperty.Register("SelectMode", typeof(SelectMode), typeof(CommandControl), new PropertyMetadata(SelectMode.Single));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(CommandControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (Command != null) {
                if (Command.CanExecute(CommandParameter)) {
                    Command.Execute(CommandParameter);
                }
            }
        }

        public CommandControl() {
            MouseLeftButtonDown += OnMouseLeftButtonDown;
        }
    }
}
