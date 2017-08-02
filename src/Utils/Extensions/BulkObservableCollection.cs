using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ApplManga.Utils.Extensions {
    public class BulkObservableCollection<T> : ObservableCollection<T> {
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

        public void AddRange(IEnumerable<T> collection) {
            if (collection == null) {
                throw new ArgumentNullException("collection");
            }

            _suppressNotification = true;
            int startIndex = Count;

            try {
                IList<T> items = Items;
                if (items != null) {
                    using (IEnumerator<T> enumerator = collection.GetEnumerator()) {
                        while (enumerator.MoveNext()) {
                            items.Add(enumerator.Current);
                        }
                    }
                }
            } finally {
                _suppressNotification = false;

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void RemoveRange(IEnumerable<T> collection) {
            if (collection == null) {
                throw new ArgumentNullException("collection");
            }

            _suppressNotification = true;
            int startIndex = Count;

            try {
                IList<T> items = Items;
                if (items != null) {
                    using (IEnumerator<T> enumerator = collection.GetEnumerator()) {
                        while (enumerator.MoveNext()) {
                            items.Remove(enumerator.Current);
                        }
                    }
                }
            } finally {
                _suppressNotification = false;

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public List<T> GetRange(int index, int count) {
            if (index < 0) {
                throw new ArgumentOutOfRangeException("index");
            }

            if (count < 0) {
                throw new ArgumentOutOfRangeException("count");
            }

            if ((Count - index) < count) {
                throw new ArgumentException("Invalid offset length");
            }

            return new List<T>(Items.Skip(index).Take(count));
        }
    }
}
