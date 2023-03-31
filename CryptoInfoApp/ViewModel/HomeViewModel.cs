using CoinGecko.Clients;
using CoinGecko.Entities.Coins;
using CryptoInfoApp.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System;

namespace CryptoInfoApp.ViewModel
{
    public class HomeViewModel : ObservableObject, IDisposable
    {
        private CoinGeckoClient Api { get; } = new();
        public ObservableCollection<CoinMarkets> Coins { get; set; } = new();
        public decimal MarketCapChangePercentage24h { get; set; }
        public AsyncRelayCommand CoinsCommand { get; set; }
        public Visibility LoadingVisibility { get; set; } = Visibility.Visible;
        public Visibility CircleLoaderVisibility { get; set; } = Visibility.Visible;
        public Visibility ErrorVisibility { get; set; } = Visibility.Hidden;
        public string ErroeMessage { get; set; } = string.Empty;

        private Task<List<CoinMarkets>> GetCoins(int size, int page)
        {
            return Api.CoinsClient.GetCoinMarkets(vsCurrency: "usd", ids: Array.Empty<string>(), order: "market_cap_desc", perPage: size, page: page, sparkline: true, priceChangePercentage: "24h,7d", category: "");
        }

        private async Task FetchData(int page = 1)
        {
            try
            {
                var coins = await GetCoins(size: 30, page);
                var globalTask = Api.GlobalClient.GetGlobal();
                var globalData = await globalTask;
                MarketCapChangePercentage24h = globalData.Data.MarketCapChange;
                LoadingVisibility = Visibility.Hidden;
            }
            catch (HttpRequestException ex)
            {
                ErroeMessage = ex.Message;
                LoadingVisibility = Visibility.Hidden;
                ErrorVisibility = Visibility.Visible;
            }
        }

        public HomeViewModel()
        {
            CoinsCommand = new AsyncRelayCommand(() => FetchData());
            Task.Run(async () =>
            {
                await FetchData();
            });
        }

        public void Dispose()
        {
            Api.Dispose();
        }
    }
}