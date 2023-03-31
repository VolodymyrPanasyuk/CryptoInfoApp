using CoinGecko.Clients;
using CoinGecko.Entities.Coins;
using CryptoInfoApp.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System;
using CoinCap.Clients;
using CryptoInfoApp.Model;
using CoinCap.Entities.Assets;

namespace CryptoInfoApp.ViewModel
{
    public class HomeViewModel : ObservableObject, IDisposable
    {
        private CoinGeckoClient CoinGeckoApi { get; } = new();
        private CoinCapClient CoinCapApi { get; } = new();
        public ObservableCollection<HomeModel> HomeModels { get; set; } = new();

        public Visibility LoadingVisibility { get; set; } = Visibility.Visible;
        public Visibility ErrorVisibility { get; set; } = Visibility.Collapsed;
        public bool CircleIconSpin { get; set; } = true;
        public string StatusMessage { get; set; } = "Loading";
        public string CoinGeckoErrorMessage { get; set; }
        public string CoinCapErrorMessage { get; set; }

        private List<HomeModel> ConvertToHomeModels(List<CoinMarkets> coinGeckoData)
        {
            List<HomeModel> homeModels = new();
            foreach (var coin in coinGeckoData)
            {
                var homeModel = new HomeModel
                {
                    Id = coin.Id,
                    Image = coin.Image,
                    Name = coin.Name,
                    Symbol = coin.Symbol,
                    Price = coin.CurrentPrice,
                    PriceChangePercentage24H = coin.PriceChangePercentage24HInCurrency,
                    PriceChangePercentage7D = coin.PriceChangePercentage7DInCurrency,
                    MarketCap = coin.MarketCap,
                    Volume24H = coin.TotalVolume,
                    CirculatingSupply = coin.CirculatingSupply
                };

                homeModels.Add(homeModel);
            }
            return homeModels;
        }

        private List<HomeModel> ConvertToHomeModels(List<AssetById> coinCapData)
        {
            List<HomeModel> homeModels = new();
            foreach (var asset in coinCapData)
            {
                var homeModel = new HomeModel
                {
                    Id = asset.Id,
                    Image = null,
                    Name = asset.Name,
                    Symbol = asset.Symbol,
                    Price = asset.PriceUsd,
                    PriceChangePercentage24H = asset.ChangePercent24Hr,
                    PriceChangePercentage7D = null,
                    MarketCap = asset.MarketCapUsd,
                    Volume24H = asset.VolumeUsd24Hr,
                    CirculatingSupply = asset.Supply
                };

                homeModels.Add(homeModel);
            }
            return homeModels;
        }

        private Task<List<CoinMarkets>> GetFromCoinGeckoApi(int size, int page)
        {
            return CoinGeckoApi.CoinsClient.GetCoinMarkets(vsCurrency: "usd", ids: Array.Empty<string>(), order: "market_cap_desc", perPage: size, page: page, sparkline: true, priceChangePercentage: "24h,7d", category: "");
        }

        private Task<List<AssetById>> GetFromCoinCapApi(int limit)
        {
            return CoinCapApi.AssetsClient.GetAllAssets(null, null, limit, null);
        }

        private async Task FetchData()
        {
            try
            {
                var coins = await GetFromCoinGeckoApi(30, 1);

                DispatcherHelper.Invoke(() =>
                {
                    HomeModels.Clear();
                    ConvertToHomeModels(coins).ForEach(c => HomeModels.Add(c));
                    LoadingVisibility = Visibility.Collapsed;
                });
            }
            catch (HttpRequestException ex)
            {
                DispatcherHelper.Invoke(() =>
                {
                    CoinGeckoErrorMessage = "CoinGecko returns: " + ex.Message;
                    ErrorVisibility = Visibility.Visible;
                    StatusMessage = "Trying to get data from other API";
                });

                try
                {
                    var data = await GetFromCoinCapApi(30);

                    DispatcherHelper.Invoke(() =>
                    {
                        HomeModels.Clear();
                        ConvertToHomeModels(data).ForEach(c => HomeModels.Add(c));
                        LoadingVisibility = Visibility.Collapsed;
                    });
                }
                catch (HttpRequestException ex2)
                {
                    DispatcherHelper.Invoke(() =>
                    {
                        CoinCapErrorMessage = "CoinCap returns: " + ex2.Message;
                        CircleIconSpin = false;
                        StatusMessage = "Both APIs return an error. Click on the \"Reload page\" button to try again";
                    });
                }
            }
        }

        public HomeViewModel()
        {
            Task.Run(async () => await FetchData());
        }

        public void Dispose()
        {
            CoinGeckoApi.Dispose();
            CoinCapApi.Dispose();
        }
    }
}