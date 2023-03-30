using CryptoInfoApp.Core;

namespace CryptoInfoApp.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CoinViewCommand { get; set; }
        public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand ConvertViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public CoinViewModel CoinVM { get; set; }
        public SearchViewModel SearchVM { get; set; }
        public ConvertViewModel ConvertVM { get; set; }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVM != null ? HomeVM : new HomeViewModel();
            });

            CoinViewCommand = new RelayCommand(x =>
            {
                CurrentView = CoinVM != null ? CoinVM : new CoinViewModel();
            });

            SearchViewCommand = new RelayCommand(x =>
            {
                CurrentView = SearchVM != null ? SearchVM : new SearchViewModel();
            });

            ConvertViewCommand = new RelayCommand(x =>
            {
                CurrentView = ConvertVM != null ? ConvertVM : new ConvertViewModel();
            });
        }
    }
}