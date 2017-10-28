using jdx.ApplManga.Core.ViewModels;
using System.Collections.Generic;

namespace jdx.ApplManga.Controls.SideMenuEx {
    /// <summary>
    /// Design-time data for <see cref="NavMenuListViewModel"/>
    /// For design preview only
    /// </summary>
    public class SideMenuItemsListDesignModel : NavMenuListViewModel {
        public static SideMenuItemsListDesignModel Instance => new SideMenuItemsListDesignModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuItemsListDesignModel() {
            Items = new List<NavMenuItemsViewModel>() {
                new NavMenuItemsViewModel {
                    MenuItemIcon = "\xE896",
                    MenuItemName = "DOWNLOADS"
                },
                new NavMenuItemsViewModel {
                    MenuItemIcon = "\xE82D",
                    MenuItemName = "BROWSE",
                    IsSelected = true
                },
                new NavMenuItemsViewModel {
                    MenuItemIcon = "\xEB52",
                    MenuItemName = "FAVORITES"
                },
                new NavMenuItemsViewModel {
                    MenuItemIcon = "\xED43",
                    MenuItemName = "FOLDERS"
                }
            };
        }
    }
}
