﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CloudsdaleWin7.MVVM {
    class StringLinesplitConverter : IValueConverter {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value.ToString().Split('\n').Select(line => line.Trim()).ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }

        #endregion
    }
}
