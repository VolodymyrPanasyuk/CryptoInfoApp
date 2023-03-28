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

        public RelayCommand ConvertViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }

        public CoinViewModel CoinVM { get; set; }

        public ConvertViewModel ConvertVM { get; set; }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CoinVM = new CoinViewModel();
            ConvertVM = new ConvertViewModel();

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVM;
            });

            CoinViewCommand = new RelayCommand(x =>
            {
                CurrentView = CoinVM;
            });

            ConvertViewCommand = new RelayCommand(x =>
            {
                CurrentView = ConvertVM;
            });

            CurrentView = HomeVM;
        }
    }
}