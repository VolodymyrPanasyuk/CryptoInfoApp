using CryptoInfoApp.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CryptoInfoApp.View
{
    public partial class CoinView : UserControl
    {
        public CoinView()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow.DataContext as MainViewModel).CurrentView;
        }
    }
}