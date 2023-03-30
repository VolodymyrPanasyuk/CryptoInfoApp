using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptoInfoApp.Core.Converters
{
    public class PercentageToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            decimal percentage = (decimal)value;
            return (percentage >= 0 ? "⏶" : "⏷") + Math.Abs(percentage / 100).ToString("P", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}