using System.Windows;

namespace CryptoInfoApp.Core
{
    public class Loader : ObservableObject
    {
        private bool _isLoaded;
        private Visibility _loadingVisibility;
        private Visibility _errorVisibility;
        private bool _circleIconSpin;
        private string _statusMessage;
        private string _coinGeckoErrorMessage;
        private string _coinCapErrorMessage;

        public bool IsLoaded
        {
            get => _isLoaded;
            set
            {
                _isLoaded = value;
                OnPropertyChanged(nameof(IsLoaded));
            }
        }
        public Visibility LoadingVisibility
        {
            get => _loadingVisibility;
            set
            {
                _loadingVisibility = value;
                OnPropertyChanged(nameof(LoadingVisibility));
            }
        }
        public Visibility ErrorVisibility
        {
            get => _errorVisibility;
            set
            {
                _errorVisibility = value;
                OnPropertyChanged(nameof(ErrorVisibility));
            }
        }
        public bool CircleIconSpin
        {
            get => _circleIconSpin;
            set
            {
                _circleIconSpin = value;
                OnPropertyChanged(nameof(CircleIconSpin));
            }
        }
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        public string CoinGeckoErrorMessage
        {
            get => _coinGeckoErrorMessage;
            set
            {
                _coinGeckoErrorMessage = value;
                OnPropertyChanged(nameof(CoinGeckoErrorMessage));
            }
        }
        public string CoinCapErrorMessage
        {
            get => _coinCapErrorMessage;
            set
            {
                _coinCapErrorMessage = value;
                OnPropertyChanged(nameof(CoinCapErrorMessage));
            }
        }

        public void CoinGeckoError(string message)
        {
            CoinGeckoErrorMessage = "CoinGecko: " + message;
            StatusMessage = "Trying to get data from other API";
            ErrorVisibility = Visibility.Visible;
        }

        public void CoinCapError(string message)
        {
            CoinCapErrorMessage = "CoinCap: " + message;
            ErrorVisibility = Visibility.Visible;
        }

        public void LoadingFailed(string message = "Failed to get data from APIs. Click on the \"Reload page\" button to try again")
        {
            CircleIconSpin = false;
            StatusMessage = message;
        }

        public void LoadingSuccess()
        {
            LoadingVisibility = Visibility.Collapsed;
            IsLoaded = true;
        }

        public Loader(string statusMessage = "Loading", bool circleIconSpin = true)
        {
            _isLoaded = false;
            _loadingVisibility = Visibility.Visible;
            _errorVisibility = Visibility.Collapsed;
            _circleIconSpin = circleIconSpin;
            _statusMessage = statusMessage;
            _coinGeckoErrorMessage = "";
            _coinCapErrorMessage = "";
        }
    }
}