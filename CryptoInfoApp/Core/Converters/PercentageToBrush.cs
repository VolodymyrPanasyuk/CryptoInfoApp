﻿using System;
using System.Globalization;
using System.Windows.Data;
using CryptoInfoApp.Properties;

namespace CryptoInfoApp.Core.Converters
{
    public class PercentageToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (Settings.Default.DarkTheme)
                {
                    return (decimal)value >= 0 ? "#00db0f" : "#ff0000";
                }
                else
                {
                    return (decimal)value >= 0 ? "#006607" : "#b30000";
                }
            }
            return "Transparent";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}