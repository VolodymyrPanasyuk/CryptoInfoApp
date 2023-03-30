using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptoInfoApp.Core.Converters
{
    public class DecimalToShortCurrency : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal v = (decimal)value;
            culture = CultureInfo.CurrentCulture;
            string symbol = culture.NumberFormat.CurrencySymbol;
            return v switch
            {
                > 999_999_999 => v.ToString(symbol + "0,,,.##B", culture),
                > 999_999 => v.ToString(symbol + "0,,.##M", culture),
                > 9_999 => v.ToString(symbol + "0,.##K", culture),
                _ => v.ToString("C", culture)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}