using System;
using System.Diagnostics;
using System.Globalization;
using jdx.ApplManga.Core.Models;
using jdx.ApplManga.Views;

namespace jdx.ApplManga.Converters {
    /// <summary>
    /// Converts <see cref="AppView"/> to an actual view
    /// </summary>
    public class AppViewValueConverter : BaseValueConverter<AppViewValueConverter> {
        public override object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null) {
            switch ((AppView)value) {
                case AppView.Downloads:
                    return new DownloadsView();
                case AppView.Browse:
                    return new BrowseView();
                case AppView.Info:
                    return new InfoView();
                case AppView.Favorites:
                    return new FavoritesView();
                case AppView.Folders:
                    return new FoldersView();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
