using CryptoInfoApp.Model;
using CryptoInfoApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CryptoInfoApp.View
{
    public partial class ConvertView : UserControl
    {
        public ConvertView()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow.DataContext as MainViewModel).CurrentView;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (FromCoinComboBox.SelectedIndex != -1 && ToCoinComboBox.SelectedIndex != -1 && !string.IsNullOrEmpty(Count.Text))
            {
                decimal count;
                if (decimal.TryParse(Count.Text.Replace(',', '.'), out count))
                {
                    var from = FromCoinComboBox.SelectedItem as ConvertModel;
                    var to = ToCoinComboBox.SelectedItem as ConvertModel;
                    decimal result = (decimal)((from.Price * count) / to.Price);
                    Result.Content = "Result: " + Math.Round(result, 10) + " " + to.Symbol;
                }
                else
                {
                    Result.Content = "Result:";
                }
            }
        }
    }
}