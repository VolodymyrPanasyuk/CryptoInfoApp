using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CryptoInfoApp.Core.Converters
{
    public class ImageToSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(ImageSource))
            {
                if (value != null)
                {
                    return value;
                }
            }
            return Binding.DoNothing;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}