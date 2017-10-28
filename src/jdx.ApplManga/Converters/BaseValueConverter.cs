using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace jdx.ApplManga.Converters {
    /// <summary>
    /// Base value converter to allow direct XAML usage
    /// </summary>
    /// <typeparam name="T">Type of value converter</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T : class, new() {
        private static T converter = null;

        /// <summary>
        /// Provides a static instance of this converter
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return converter ?? (converter = new T());
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
