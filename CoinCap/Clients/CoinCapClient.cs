using CoinCap.Interfaces;
using CoinGecko.ApiEndPoints;

namespace CoinCap.Clients
{
    public partial class CoinCapClient : IDisposable, ICoinCapClient
    {
        private static readonly Lazy<CoinCapClient> Lazy = new(() => new CoinCapClient());

        private readonly HttpClient _httpClient;
        private bool _isDisposed;

        public CoinCapClient()
        {
            _httpClient = new();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {BaseApiEndPointUrl.ApiKey}");
        }

        public IAssetsClient AssetsClient => new AssetsClient(_httpClient);
        public IRatesClient RatesClient => new RatesClient(_httpClient);
        public IExchangesClient ExchangesClient => new ExchangesClient(_httpClient);
        public IMarketsClient MarketsClient => new MarketsClient(_httpClient);
        public ICandlesClient CandlesClient => new CandlesClient(_httpClient);

        public static CoinCapClient Instance => Lazy.Value;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                _httpClient?.Dispose();
            }
            _isDisposed = true;
        }
    }
}