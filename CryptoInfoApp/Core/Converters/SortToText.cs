using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CryptoInfoApp.Core.Converters
{
    public class SortToText : IMultiValueConverter
    {
        private static string Prefix(ListSortDirection direction) => direction == ListSortDirection.Ascending ? "⏶" : "⏷";

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var column = values[0] as string;
            var sortedBy = values[1] as string;
            var sortDirection = (ListSortDirection)values[2];
            var alignment = HorizontalAlignment.Right;
            if (values[3] is HorizontalAlignment a) alignment = a;

            string format = alignment == HorizontalAlignment.Right ? $"{Prefix(sortDirection)}{column}" : $"{column}{Prefix(sortDirection)}";
            return sortedBy == column ? format : column;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}