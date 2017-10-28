using System;
using System.Windows.Input;

namespace jdx.ApplManga.Core.ViewModels {
    public class RelayCommand : ICommand {
        //private readonly Action<object> _execute;
        //private readonly Predicate<object> _canExecute;

        //public RelayCommand(Action<object> execute, Predicate<object> canExecute = null) {
        //    _execute = execute ?? throw new ArgumentNullException("execute");
        //    _canExecute = canExecute;
        //}

        //public RelayCommand(Action<object> execute) {
        //    _execute = execute;
        //}

        //public event EventHandler CanExecuteChanged {
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        //public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        //public void Execute(object parameter) => _execute(parameter ?? "<NA>");

        private Action _action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action) {
            _action = action;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _action();
        }
    }
}
