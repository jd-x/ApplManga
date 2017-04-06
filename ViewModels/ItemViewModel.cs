using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplManga.ViewModels {
    public class ItemViewModel : ViewModelBase {
        private string name;

        public ItemViewModel(string tabName) {
            TabName = tabName;
        }

        public string TabName {
            get; private set;
        }

        public string Name {
            get {
                return name;
            }

            set {
                if (name != value) {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
    }
}
