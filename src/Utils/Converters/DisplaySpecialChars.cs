using System;
using System.Globalization;
using System.Windows.Data;

namespace ApplManga.ViewModels.Converters {
    public class DisplaySpecialChars : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string outputString = string.Empty;
            string inputString = value as string;

            if(value != null) {
                string copyright = "\u00A9";
            }

            return outputString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
