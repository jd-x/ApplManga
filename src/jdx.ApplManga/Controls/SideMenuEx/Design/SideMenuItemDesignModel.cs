using jdx.ApplManga.Core.ViewModels;

namespace jdx.ApplManga.Controls.SideMenuEx {
    /// <summary>
    /// Design-time data for <see cref="NavMenuItemsViewModel"/>
    /// For design preview only
    /// </summary>
    public class SideMenuItemDesignModel : NavMenuItemsViewModel {
        public static SideMenuItemDesignModel Instance => new SideMenuItemDesignModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuItemDesignModel() {
            MenuItemIcon = "\xE82D";
            MenuItemName = "BROWSE";
        }
    }
}
