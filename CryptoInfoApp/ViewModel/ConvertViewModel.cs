using CoinCap.Clients;
using CoinCap.Entities.Assets;
using CoinCap.Entities;
using CoinGecko.Clients;
using CoinGecko.Entities.Coins;
using CryptoInfoApp.Core;
using CryptoInfoApp.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Windows;

namespace CryptoInfoApp.ViewModel
{
    public class ConvertViewModel : ObservableObject
    {
        private CoinGeckoClient CoinGeckoApi { get; } = new();
        private CoinCapClient CoinCapApi { get; } = new();

        public ObservableCollection<ConvertModel> ConvertModels { get; set; } = new();
        public Loader Loader { get; set; }

        private List<ConvertModel> ConvertToConvertModels(List<CoinMarkets> coinGeckoData)
        {
            List<ConvertModel> convertModels = new();
            foreach (var coin in coinGeckoData)
            {
                var convertModel = new ConvertModel
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

                convertModels.Add(convertModel);
            }
            return convertModels;
        }

        private List<ConvertModel> ConvertToConvertModels(ApiResponseArray<AssetById> coinCapData)
        {
            List<ConvertModel> convertModels = new();
            if (coinCapData.Data == null)
                return convertModels;

            var numberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };
            foreach (var asset in coinCapData.Data)
            {
                var convertModel = new ConvertModel
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    Symbol = asset.Symbol,
                };

                if (long.TryParse(asset.Rank, out long rank))
                    convertModel.Rank = rank;

                if (decimal.TryParse(asset.PriceUsd, NumberStyles.Any, numberFormat, out decimal priceUsd))
                    convertModel.Price = priceUsd;

                if (decimal.TryParse(asset.ChangePercent24Hr, NumberStyles.Any, numberFormat, out decimal priceChangePercentage24H))
                    convertModel.PriceChangePercentage24H = priceChangePercentage24H;

                if (decimal.TryParse(asset.MarketCapUsd, NumberStyles.Any, numberFormat, out decimal marketCapUsd))
                    convertModel.MarketCap = marketCapUsd;

                if (decimal.TryParse(asset.VolumeUsd24Hr, NumberStyles.Any, numberFormat, out decimal volumeUsd24Hr))
                    convertModel.Volume24H = volumeUsd24Hr;

                if (decimal.TryParse(asset.Supply, NumberStyles.Any, numberFormat, out decimal circulatingSupply))
                    convertModel.CirculatingSupply = circulatingSupply;

                convertModels.Add(convertModel);
            }

            return convertModels;
        }

        private Task<List<CoinMarkets>> GetFromCoinGeckoApi()
        {
            return CoinGeckoApi.CoinsClient.GetCoinMarkets(vsCurrency: "usd", ids: Array.Empty<string>(), order: "market_cap_desc", perPage: 250, page: 1, sparkline: false, priceChangePercentage: "", category: "");
        }

        private Task<ApiResponseArray<AssetById>> GetFromCoinCapApi()
        {
            return CoinCapApi.AssetsClient.GetAssets(250, 0);
        }

        private async Task FetchData()
        {
            try
            {
                var coinGeckoResponse = await GetFromCoinGeckoApi();

                await Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    ConvertToConvertModels(coinGeckoResponse).ForEach(c => ConvertModels.Add(c));
                });

                Loader.LoadingSuccess();
            }
            catch (HttpRequestException ex)
            {
                Loader.CoinGeckoError(ex.Message);
                try
                {
                    var coinCapResponse = await GetFromCoinCapApi();

                    await Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        ConvertToConvertModels(coinCapResponse).ForEach(c => ConvertModels.Add(c));
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

        public ConvertViewModel()
        {
            Loader = new Loader();
            var usd = new ConvertModel()
            {
                Id = "usd",
                Name = "United States Dollar",
                Symbol = "USD",
                Price = 1
            };
            ConvertModels.Add(usd);
            Task.Run(async () => await FetchData());
        }

        public void Dispose()
        {
            CoinGeckoApi.Dispose();
            CoinCapApi.Dispose();
        }
    }
}