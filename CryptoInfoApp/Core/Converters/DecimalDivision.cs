using System.Globalization;
using System.Windows.Data;
using System;

namespace CryptoInfoApp.Core.Converters
{
    public class DecimalDivision : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0:N5}",
                values[0] is not decimal a || values[1] is not decimal b ? 0 : b / a)
           ;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}