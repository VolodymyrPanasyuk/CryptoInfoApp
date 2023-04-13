using CryptoInfoApp.Model;
using CryptoInfoApp.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CryptoInfoApp.View
{
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow.DataContext as MainViewModel).CurrentView;
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedCoin = (sender as DataGrid)?.SelectedItem as SearchModel;
            var mainViewModel = Application.Current.MainWindow.DataContext as MainViewModel;
            mainViewModel.CoinViewWithIdCommand.Execute(selectedCoin.Id);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.SetRadioButtonBasedOnCurrentVM(typeof(CoinViewModel));
        }
    }
}