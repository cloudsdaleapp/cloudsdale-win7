﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Cloudsdale_Win7.MVVM {
    public class ChatColorConverter : IValueConverter {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new SolidColorBrush(value != null && value.ToString().StartsWith("> ") ? Colors.ForestGreen : Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new InvalidOperationException("Cannot convert message color back to the message!");
        }

        #endregion
    }
}
