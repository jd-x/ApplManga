using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;

namespace jdx.ApplManga.Utils.Extensions {
    public class CheckedObservableCollection<T> : ObservableCollection<CheckedListBoxItem<T>> {
        private ListCollectionView _selected;

        private bool _suppressNotification = false;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            if (!_suppressNotification) {
                base.OnCollectionChanged(e);
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
            if (!_suppressNotification) {
                base.OnPropertyChanged(e);
            }
        }

        public CheckedObservableCollection() {
            _selected = new ListCollectionView(this);
            _selected.Filter = delegate (object checkedItem) {
                return ((CheckedListBoxItem<T>)checkedItem).IsChecked;
            };
        }

        public void Add(T item) {
            Add(new CheckedListBoxItem<T>(this) { Item = item });
        }

        public void AddRange(IEnumerable<T> collection) {
            if (collection == null) {
                throw new ArgumentNullException("collection");
            }

            _suppressNotification = true;

            try {
                if (collection != null) {
                    foreach (T item in collection) {
                        Add(item);
                    }
                }
            } finally {
                _suppressNotification = false;

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void Remove(T item) {
            Remove(new CheckedListBoxItem<T>(this) { Item = item });
        }

        public void RemoveRange(IEnumerable<T> collection) {
            if (collection == null) {
                throw new ArgumentNullException("collection");
            }

            _suppressNotification = true;

            try {
                if (collection != null) {
                    foreach (T item in collection) {
                        Remove(item);
                    }
                }
            } finally {
                _suppressNotification = false;

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /*public List<T> GetRange(int index, int count) {
            if (index < 0) {
                throw new ArgumentOutOfRangeException("index");
            }

            if (count < 0) {
                throw new ArgumentOutOfRangeException("count");
            }

            if ((Count - index) < count) {
                throw new ArgumentException("Invalid offset length");
            }

            return new List<T>(Items.ToList().GetRange(index, count));
        }*/

        public void Refresh() {
            _selected.Refresh();
        }

        public ICollectionView SelectedItems {
            get { return _selected; }
        }
    }
}
