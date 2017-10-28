using System;
using System.Globalization;

namespace jdx.ApplManga.Converters {
    /// <summary>
    /// Returns half the value of the given dimension
    /// </summary>
    public class HalfSizeValueConverter : BaseValueConverter<HalfSizeValueConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToInt32(value) / System.Convert.ToInt32(parameter);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
