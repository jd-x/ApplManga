using ApplManga.ViewModels;

namespace ApplManga.Utils.Extensions {
    public class CheckedListBoxItem<T> : ViewModelBase {
        private readonly CheckedObservableCollection<T> _parent;

        public CheckedListBoxItem(CheckedObservableCollection<T> parent) {
            _parent = parent;
        }

        private T _item;
        public T Item {
            get { return _item; }
            set {
                _item = value;
                OnPropertyChanged("Item");
            }
        }

        private bool _isChecked;
        public bool IsChecked {
            get { return _isChecked; }
            set {
                _isChecked = value;
                CheckChanged();
                OnPropertyChanged("IsChecked");
            }
        }

        private void CheckChanged() {
            // Don't call refresh every time an item is checked
            _parent.Refresh();
        }
    }
}
