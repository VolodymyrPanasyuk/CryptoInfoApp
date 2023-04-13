using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptoInfoApp.Core.Converters
{
    public class DecimalToZeroCurrency : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return ((decimal)value).ToString("C0", CultureInfo.CurrentCulture);
            }
            else
            {
                return "No data";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}