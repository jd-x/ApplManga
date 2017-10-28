using System;
using System.Windows.Input;

namespace jdx.ApplManga.Core.ViewModels {
    public class RelayParamCommand : ICommand {
        private Action<object> _action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayParamCommand(Action<object> action) {
            _action = action;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _action(parameter);
        }
    }
}
