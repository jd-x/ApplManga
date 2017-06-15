namespace ApplManga.Commands {
    using System;
    using System.Windows.Input;
    using ViewModels;

    internal class CustomerUpdateCommand : ICommand {
        public CustomerUpdateCommand(CustomerViewModel viewModel) {
            this.viewModel = viewModel;
        }

        private CustomerViewModel viewModel;

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            return String.IsNullOrWhiteSpace(viewModel.Customer.Error);
        }

        public void Execute(object parameter) {
            viewModel.SaveChanges();
        }

        #endregion
    }
}
