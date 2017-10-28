using System.Collections.Generic;

namespace jdx.ApplManga.Core.ViewModels {
    /// <summary>
    /// ViewModel for the ListView ComboBox list
    /// </summary>
    public class ListBoxComboListViewModel : BaseViewModel {
        /// <summary>
        /// ComboBox collection list
        /// </summary>
        public List<ListBoxComboItemsViewModel> ComboItems { get; set; }
    }
}
