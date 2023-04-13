using CoinCap.Clients;
using CoinCap.Entities.Assets;
using CoinCap.Entities;
using CoinGecko.Clients;
using CoinGecko.Entities.Coins;
using CryptoInfoApp.Core;
using CryptoInfoApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows;
using System.Linq;

namespace CryptoInfoApp.ViewModel
{
    public class SearchViewModel : ObservableObject, IDisposable
    {
        private CoinGeckoClient CoinGeckoApi { get; } = new();
        private CoinCapClient CoinCapApi { get; } = new();

        private List<(string Id, string Symbol, string Name)> _coinGeckoSearchList = new();
        private List<(string Id, string Symbol, string Name)> _coinCapSearchList = new();

        public ObservableCollection<SearchModel> SearchModels { get; set; } = new();
        public Loader Loader { get; set; }

        private async Task FillSearchLists()
        {
            try
            {
                var coinGeckoResponse = await CoinGeckoApi.CoinsClient.GetCoinList();

                foreach (var coin in coinGeckoResponse)
                {
                    _coinGeckoSearchList.Add((coin.Id, coin.Symbol, coin.Name));
                }
            }
            catch (HttpRequestException ex)
            {
                Loader.CoinGeckoError(ex.Message);
            }

            try
            {
                var coinCapResponse = await CoinCapApi.AssetsClient.GetAssets(2000, 0);

                foreach (var asset in coinCapResponse.Data)
                {
                    _coinCapSearchList.Add((asset.Id, asset.Symbol, asset.Name));
                }
            }
            catch (HttpRequestException ex2)
            {
                Loader.CoinCapError(ex2.Message);
            }

            if (Loader.CoinGeckoErrorMessage != "" && Loader.CoinCapErrorMessage != "")
            {
                Loader.LoadingFailed("Something went wrong, please try again");
            }
        }

        private (string[] coinGeckoIds, string[] coinCapIds) Search(string userInput)
        {
            List<string> coinGeckoSearchResult = new();
            List<string> coinCapSearchResult = new();

            string search = userInput.Trim().ToLower();

            foreach (var tuple in _coinGeckoSearchList)
            {
                if (tuple.Id.ToLower() == search || tuple.Symbol.ToLower() == search || tuple.Name.ToLower() == search)
                {
                    coinGeckoSearchResult.Add(tuple.Id);
                }
                else if (tuple.Symbol.ToLower().Contains(search) || tuple.Name.ToLower().Contains(search))
                {
                    coinGeckoSearchResult.Add(tuple.Id);
                }
            }

            foreach (var tuple in _coinCapSearchList)
            {
                if (tuple.Id.ToLower() == search || tuple.Symbol.ToLower() == search || tuple.Name.ToLower() == search)
                {
                    coinCapSearchResult.Add(tuple.Id);
                }
                else if (tuple.Symbol.ToLower().Contains(search) || tuple.Name.ToLower().Contains(search))
                {
                    coinCapSearchResult.Add(tuple.Id);
                }
            }

            return (coinGeckoSearchResult.Distinct().ToArray(), coinCapSearchResult.Distinct().ToArray());
        }

        private List<SearchModel> ConvertToSearchModels(List<CoinMarkets> coinGeckoData)
        {
            List<SearchModel> searchModels = new();
            foreach (var coin in coinGeckoData)
            {
                var searchModel = new SearchModel
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

                searchModels.Add(searchModel);
            }
            return searchModels;
        }

        private List<SearchModel> ConvertToSearchModels(ApiResponseArray<AssetById> coinCapData)
        {
            List<SearchModel> searchModels = new();
            if (coinCapData.Data == null)
                return searchModels;

            var numberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };
            foreach (var asset in coinCapData.Data)
            {
                var searchModel = new SearchModel
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    Symbol = asset.Symbol,
                };

                if (long.TryParse(asset.Rank, out long rank))
                    searchModel.Rank = rank;

                if (decimal.TryParse(asset.PriceUsd, NumberStyles.Any, numberFormat, out decimal priceUsd))
                    searchModel.Price = priceUsd;

                if (decimal.TryParse(asset.ChangePercent24Hr, NumberStyles.Any, numberFormat, out decimal priceChangePercentage24H))
                    searchModel.PriceChangePercentage24H = priceChangePercentage24H;

                if (decimal.TryParse(asset.MarketCapUsd, NumberStyles.Any, numberFormat, out decimal marketCapUsd))
                    searchModel.MarketCap = marketCapUsd;

                if (decimal.TryParse(asset.VolumeUsd24Hr, NumberStyles.Any, numberFormat, out decimal volumeUsd24Hr))
                    searchModel.Volume24H = volumeUsd24Hr;

                if (decimal.TryParse(asset.Supply, NumberStyles.Any, numberFormat, out decimal circulatingSupply))
                    searchModel.CirculatingSupply = circulatingSupply;

                searchModels.Add(searchModel);
            }

            return searchModels;
        }

        private Task<List<CoinMarkets>> GetFromCoinGeckoApi(string[] coinsiId)
        {
            return CoinGeckoApi.CoinsClient.GetCoinMarkets(vsCurrency: "usd", ids: coinsiId, order: "market_cap_desc", perPage: 100, page: 1, sparkline: true, priceChangePercentage: "24h,7d", category: "");
        }

        private Task<ApiResponseArray<AssetById>> GetFromCoinCapApi(string[] assetsId)
        {
            return CoinCapApi.AssetsClient.GetAssets(null, assetsId, 100, 0);
        }

        private async Task FetchData(string[] coinGeckoIds, string[] coinCapIds)
        {
            try
            {
                var coinGeckoResponse = await GetFromCoinGeckoApi(coinGeckoIds);

                await Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    ConvertToSearchModels(coinGeckoResponse).ForEach(c => SearchModels.Add(c));
                });

                Loader.LoadingSuccess();
            }
            catch (HttpRequestException ex)
            {
                Loader.CoinGeckoError(ex.Message);
                try
                {
                    var coinCapResponse = await GetFromCoinCapApi(coinCapIds);

                    await Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        ConvertToSearchModels(coinCapResponse).ForEach(c => SearchModels.Add(c));
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

        public SearchViewModel(string userInput)
        {
            Loader = new Loader();

            Task.Run(async () =>
            {
                await FillSearchLists();

                if (_coinCapSearchList.Count > 0 || _coinGeckoSearchList.Count > 0)
                {
                    var searchResults = Search(userInput);
                    await FetchData(searchResults.coinGeckoIds, searchResults.coinCapIds);
                }
            });
        }

        public SearchViewModel()
        {
            Loader = new Loader("To display search results, write a name or symbol in the search bar and click the \"Search\" button", false);
        }

        public void Dispose()
        {
            CoinGeckoApi.Dispose();
            CoinCapApi.Dispose();
        }
    }
}