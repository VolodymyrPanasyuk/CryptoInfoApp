using CryptoInfoApp.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptoInfoApp.Core.Converters
{
    public class CurrencyVolumeToVolume : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = value as HomeModel;
            if (model != null)
            {
                if (model.Volume24H != null && model.Price != null)
                {
                    return string.Format(CultureInfo.CurrentCulture, "{0:N0} {1}", model.Volume24H / model.Price, model.Symbol.ToUpper());
                }
            }
            return string.Format(CultureInfo.CurrentCulture, "");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}