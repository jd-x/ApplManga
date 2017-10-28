using System.Collections.Generic;

namespace jdx.ApplManga.Core.ViewModels {
    /// <summary>
    /// ViewModel for the ListView ComboBox items
    /// </summary>
    public class ListBoxComboItemsViewModel : BaseViewModel {
        #region Public properties

        // Contains the collection of ComboBox items
        public List<string> ComboItems { get; set; }

        // The currently selected item in the Combo list
        public string SelectedItem { get; set; }

        // Sets the tag property for the ComboBox label
        public string Caption { get; set; }

        #endregion
    }
}
