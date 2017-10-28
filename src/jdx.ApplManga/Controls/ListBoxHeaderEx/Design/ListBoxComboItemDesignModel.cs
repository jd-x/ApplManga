using jdx.ApplManga.Core.ViewModels;
using System.Collections.Generic;

namespace jdx.ApplManga.Controls.ListBoxHeaderEx {
    /// <summary>
    /// Design-time data for <see cref="ListBoxComboItemsViewModel"/>
    /// For design preview only
    /// </summary>
    public class ListBoxComboItemDesignModel : ListBoxComboItemsViewModel {
        public static ListBoxComboItemDesignModel Instance => new ListBoxComboItemDesignModel();

        public ListBoxComboItemDesignModel() {
            // Populate list with items
            ComboItems = new List<string> {
                "A to Z",
                "Release year",
                "Author"
            };

            // Set initial selected item
        }
    }
}
