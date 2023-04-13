using System.Windows.Controls;
using System.Windows;
using CryptoInfoApp.ViewModel;
using CryptoInfoApp.Model;

namespace CryptoInfoApp.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow.DataContext as MainViewModel).CurrentView;
        }

        private void DataGrid_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;

            if (dataGrid != null && e.VerticalChange != 0)
            {
                if (e.VerticalOffset + e.ViewportHeight == e.ExtentHeight)
                {
                    LoadMoreButton.Visibility = Visibility.Visible;
                }
                else
                {
                    LoadMoreButton.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void LoadMoreButton_Click(object sender, RoutedEventArgs e) => LoadMoreButton.Visibility = Visibility.Collapsed;

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedCoin = (sender as DataGrid)?.SelectedItem as HomeModel;
            var mainViewModel = Application.Current.MainWindow.DataContext as MainViewModel;
            mainViewModel.CoinViewWithIdCommand.Execute(selectedCoin.Id);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetRadioButtonBasedOnCurrentVM(typeof(CoinViewModel));
        }
    }
}