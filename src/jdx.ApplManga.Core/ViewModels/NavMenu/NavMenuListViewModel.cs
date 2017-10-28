using System.Collections.Generic;

namespace jdx.ApplManga.Core.ViewModels {
    /// <summary>
    /// ViewModel for the navigation menu list
    /// </summary>
    public class NavMenuListViewModel : BaseViewModel {
        /// <summary>
        /// Navigation menu items list
        /// </summary>
        public List<NavMenuItemsViewModel> Items { get; set; }
    }
}
