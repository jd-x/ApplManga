using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace jdx.ApplManga.Controls.SearchBoxEx {
    public enum SearchMode {
        Instant,
        Regular,
    }

    public class SearchBox : TextBox {
        public static DependencyProperty SearchModeProperty = DependencyProperty.Register("SearchMode", typeof(SearchMode), typeof(SearchBox), new PropertyMetadata(SearchMode.Instant));

        public static DependencyPropertyKey HasTextPropertyKey = DependencyProperty.RegisterReadOnly("HasText", typeof(bool), typeof(SearchBox), new PropertyMetadata());
        public static DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        private static DependencyPropertyKey IsMouseLeftButtonDownPropertyKey = DependencyProperty.RegisterReadOnly("IsMouseLeftButtonDown", typeof(bool), typeof(SearchBox), new PropertyMetadata());
        public static DependencyProperty IsMouseLeftButtonDownProperty = IsMouseLeftButtonDownPropertyKey.DependencyProperty;

        public static DependencyProperty SearchEventTimeDelayProperty = DependencyProperty.Register("SearchEventTimeDelay", typeof(Duration), typeof(SearchBox), new FrameworkPropertyMetadata(new Duration(new TimeSpan(0, 0, 0, 0, 500)), new PropertyChangedCallback(OnSearchEventTimeDelayChanged)));
        private DispatcherTimer searchEventDelayTimer;

        public static readonly RoutedEvent SearchEvent = EventManager.RegisterRoutedEvent("Search", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchBox));

        // TODO: Implement generic control type
        private static Border cancelSearchIconBorder;
        private static Border searchIconBorder;

        public SearchBox() : base() {
            searchEventDelayTimer = new DispatcherTimer();
            searchEventDelayTimer.Interval = SearchEventTimeDelay.TimeSpan;
            searchEventDelayTimer.Tick += new EventHandler(OnSearchEventDelayTimerTick);
        }

        static SearchBox() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(typeof(SearchBox)));
        }

        public SearchMode SearchMode {
            get { return (SearchMode)GetValue(SearchModeProperty); }
            set { SetValue(SearchModeProperty, value); }
        }

        public bool HasText {
            get { return (bool)GetValue(HasTextProperty); }
            private set { SetValue(HasTextPropertyKey, value); }
        }

        public bool IsMouseLeftButtonDown {
            get { return (bool)GetValue(IsMouseLeftButtonDownProperty); }
            private set { SetValue(IsMouseLeftButtonDownPropertyKey, value); }
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            searchIconBorder = GetTemplateChild("PART_SearchIconBorder") as Border;
            cancelSearchIconBorder = GetTemplateChild("PART_CancelSearchIconBorder") as Border;
            List<Border> iconBorders = new List<Border>();

            // TODO: Implement a better way of getting children controls of type(x)
            iconBorders.Add(searchIconBorder);
            iconBorders.Add(cancelSearchIconBorder);

            foreach (var border in iconBorders) {
                if (border != null) {
                    border.MouseLeftButtonDown += new MouseButtonEventHandler(IconBorder_MouseLeftButtonDown);
                    border.MouseLeftButtonUp += new MouseButtonEventHandler(IconBorder_MouseLeftButtonUp);
                    border.MouseLeave += new MouseEventHandler(IconBorder_MouseLeave);
                }
            }
        }

        private void IconBorder_MouseLeave(object sender, MouseEventArgs e) {
            IsMouseLeftButtonDown = false;
        }

        private void IconBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (!IsMouseLeftButtonDown) return;

            Border iconBorder = sender as Border;
            if (iconBorder.Equals(cancelSearchIconBorder) && HasText) {
                this.Text = string.Empty;
            }

            if (HasText && SearchMode == SearchMode.Regular) {
                RaiseSearchEvent();
            }

            IsMouseLeftButtonDown = false;
        }

        private void IconBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            IsMouseLeftButtonDown = true;
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if ((e.Key == Key.Escape) && (SearchMode == SearchMode.Instant)) {
                this.Text = string.Empty;
            } else if (((e.Key == Key.Return) || (e.Key == Key.Enter)) && SearchMode == SearchMode.Regular) {
                RaiseSearchEvent();
            } else {
                base.OnKeyDown(e);
            }
        }

        public event RoutedEventHandler Search {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        private void RaiseSearchEvent() {
            if (GetBindingExpression(TextBox.TextProperty) != null) {
                GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }

            RoutedEventArgs evtArgs = new RoutedEventArgs(SearchEvent);
            RaiseEvent(evtArgs);
        }

        public Duration SearchEventTimeDelay {
            get { return (Duration)GetValue(SearchEventTimeDelayProperty); }
            set { SetValue(SearchEventTimeDelayProperty, value); }
        }

        static void OnSearchEventTimeDelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            SearchBox searchBox = d as SearchBox;
            if (searchBox != null) {
                searchBox.searchEventDelayTimer.Interval = ((Duration)e.NewValue).TimeSpan;
                searchBox.searchEventDelayTimer.Stop();
            }
        }

        void OnSearchEventDelayTimerTick(object o, EventArgs e) {
            searchEventDelayTimer.Stop();
            RaiseSearchEvent();
        }

        protected override void OnTextChanged(TextChangedEventArgs e) {
            base.OnTextChanged(e);
            HasText = Text.Length != 0;

            if (SearchMode == SearchMode.Instant) {
                searchEventDelayTimer.Stop();
                searchEventDelayTimer.Start();
            }
        }
    }
}
