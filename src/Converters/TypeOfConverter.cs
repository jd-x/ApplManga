﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace ApplManga.Converters {
    public class TypeOfConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (value == null) ? null : value.GetType();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}