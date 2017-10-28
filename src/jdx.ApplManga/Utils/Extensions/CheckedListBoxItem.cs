using jdx.ApplManga.Core.ViewModels;

namespace jdx.ApplManga.Utils.Extensions {
    public class CheckedListBoxItem<T> : BaseViewModel {
        private readonly CheckedObservableCollection<T> _parent;

        public CheckedListBoxItem(CheckedObservableCollection<T> parent) {
            _parent = parent;
        }

        private T _item;
        public T Item {
            get { return _item; }
            set {
                _item = value;
                RaisePropertyChanged(nameof(Item));
            }
        }

        private bool _isChecked;
        public bool IsChecked {
            get { return _isChecked; }
            set {
                _isChecked = value;
                CheckChanged();
                RaisePropertyChanged(nameof(IsChecked));
            }
        }

        private void CheckChanged() {
            // Don't call refresh every time an item is checked
            _parent.Refresh();
        }
    }
}
