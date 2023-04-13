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
using System.Globalization;
using CoinCap.Entities;

namespace CryptoInfoApp.ViewModel
{
    public class HomeViewModel : ObservableObject, IDisposable
    {
        private CoinGeckoClient CoinGeckoApi { get; } = new();
        private CoinCapClient CoinCapApi { get; } = new();

        private int page = 1;
        private int offset = 0;

        public ObservableCollection<HomeModel> HomeModels { get; set; } = new();
        public Loader Loader { get; set; }
        public RelayCommand LoadMoreCommand { get; set; }

        private List<HomeModel> ConvertToHomeModels(List<CoinMarkets> coinGeckoData)
        {
            List<HomeModel> homeModels = new();
            foreach (var coin in coinGeckoData)
            {
                var homeModel = new HomeModel
                {
                    Id = coin.Id,
                    Rank = coin.MarketCapRank,
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

        private List<HomeModel> ConvertToHomeModels(ApiResponseArray<AssetById> coinCapData)
        {
            List<HomeModel> homeModels = new();
            if (coinCapData.Data == null)
                return homeModels;

            var numberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };
            foreach (var asset in coinCapData.Data)
            {
                var homeModel = new HomeModel
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    Symbol = asset.Symbol,
                };

                if (long.TryParse(asset.Rank, out long rank))
                    homeModel.Rank = rank;

                if (decimal.TryParse(asset.PriceUsd, NumberStyles.Any, numberFormat, out decimal priceUsd))
                    homeModel.Price = priceUsd;

                if (decimal.TryParse(asset.ChangePercent24Hr, NumberStyles.Any, numberFormat, out decimal priceChangePercentage24H))
                    homeModel.PriceChangePercentage24H = priceChangePercentage24H;

                if (decimal.TryParse(asset.MarketCapUsd, NumberStyles.Any, numberFormat, out decimal marketCapUsd))
                    homeModel.MarketCap = marketCapUsd;

                if (decimal.TryParse(asset.VolumeUsd24Hr, NumberStyles.Any, numberFormat, out decimal volumeUsd24Hr))
                    homeModel.Volume24H = volumeUsd24Hr;

                if (decimal.TryParse(asset.Supply, NumberStyles.Any, numberFormat, out decimal circulatingSupply))
                    homeModel.CirculatingSupply = circulatingSupply;

                homeModels.Add(homeModel);
            }

            return homeModels;
        }

        private Task<List<CoinMarkets>> GetFromCoinGeckoApi(int size, int page)
        {
            return CoinGeckoApi.CoinsClient.GetCoinMarkets(vsCurrency: "usd", ids: Array.Empty<string>(), order: "market_cap_desc", perPage: size, page: page, sparkline: true, priceChangePercentage: "24h,7d", category: "");
        }

        private Task<ApiResponseArray<AssetById>> GetFromCoinCapApi(int limit, int offset)
        {
            return CoinCapApi.AssetsClient.GetAssets(limit, offset);
        }

        private async Task FetchData()
        {
            try
            {
                var coinGeckoResponse = await GetFromCoinGeckoApi(30, page);

                await Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    ConvertToHomeModels(coinGeckoResponse).ForEach(c => HomeModels.Add(c));
                });

                Loader.LoadingSuccess();
            }
            catch (HttpRequestException ex)
            {
                Loader.CoinGeckoError(ex.Message);
                try
                {
                    var coinCapResponse = await GetFromCoinCapApi(30, offset);

                    await Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        ConvertToHomeModels(coinCapResponse).ForEach(c => HomeModels.Add(c));
                    });

                    Loader.LoadingSuccess();
                }
                catch (HttpRequestException ex2)
                {
                    Loader.CoinCapError(ex2.Message);
                }
            }
            finally
            {
                if (!Loader.IsLoaded)
                    Loader.LoadingFailed();
            }
        }

        public HomeViewModel()
        {
            Loader = new Loader();
            LoadMoreCommand = new RelayCommand(x =>
            {
                page++;
                offset += 30;
                Task.Run(async () => { await FetchData(); });
            });

            Task.Run(async () => await FetchData());
        }

        public void Dispose()
        {
            CoinGeckoApi.Dispose();
            CoinCapApi.Dispose();
        }
    }
}