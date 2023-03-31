using CoinGecko.Entities.Coins;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptoInfoApp.Core.Converters
{
    public class CurrencyVolumeToVolume : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var markets = value as CoinMarkets;
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0:N0} {1}",
                markets.TotalVolume / markets.CurrentPrice,
                markets.Symbol.ToUpper()
            );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}