namespace ApplManga.ViewModels {
    using System;
    using System.Windows.Input;
    using System.Diagnostics;
    using Models;
    using Commands;

    internal class CustomerViewModel {
        private Customer customer;

        public CustomerViewModel() {
            customer = new Customer("Jade");
            UpdateCommand = new CustomerUpdateCommand(this);
        }

        public Customer Customer {
            get {
                return customer;
            }
        }

        public ICommand UpdateCommand {
            get;
            private set;
        }

        public void SaveChanges() {
            Debug.Assert(false, String.Format("{0} was updated.", Customer.Name));
        }
    }
}
