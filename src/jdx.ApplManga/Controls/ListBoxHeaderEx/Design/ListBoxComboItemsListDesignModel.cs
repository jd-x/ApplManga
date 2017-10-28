using jdx.ApplManga.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace jdx.ApplManga.Controls.ListBoxHeaderEx {
    /// <summary>
    /// Design-time data for <see cref="ListBoxComboListViewModel"/>
    /// </summary>
    public class ListBoxComboItemsListDesignModel : ListBoxComboListViewModel {
        public static ListBoxComboItemsListDesignModel Instance => new ListBoxComboItemsListDesignModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ListBoxComboItemsListDesignModel() {
            ComboItems = new List<ListBoxComboItemsViewModel>() {
                new ListBoxComboItemsViewModel {
                    ComboItems = new List<string> {
                        "A to Z",
                        "Release year",
                        "Author"
                    },
                    Caption = "Sort by:"
                },
                new ListBoxComboItemsViewModel {
                    ComboItems = new List<string> {
                        "All",
                        "Completed",
                        "Ongoing"
                    },
                    Caption = "Publishing status:"
                },
                new ListBoxComboItemsViewModel {
                    ComboItems = new List<string> {
                        "All",
                        "Bato.to",
                        "KissManga",
                        "MangaFox"
                    },
                    Caption = "Site:"
                }
            };
        }
    }
}
