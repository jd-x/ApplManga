using System;
using System.Windows.Input;

namespace jdx.ApplManga.Core.ViewModels {
    /// <summary>
    /// ViewModel for the main navigation menu items
    /// </summary>
    public class NavMenuItemsViewModel : BaseViewModel {
        #region Public properties

        public string MenuItemIcon { get; set; }

        public string MenuItemName { get; set; }

        public bool IsSelected { get; set; }

        #endregion

        #region Commands

        public ICommand SwitchToPageCommand { get; set; }

        #endregion

        private void OpenPage() {
            //IoC.Get

            Console.WriteLine(MenuItemName + " ,Is selected: " + IsSelected);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NavMenuItemsViewModel() {
            SwitchToPageCommand = new RelayCommand(OpenPage);
        }
    }
}
