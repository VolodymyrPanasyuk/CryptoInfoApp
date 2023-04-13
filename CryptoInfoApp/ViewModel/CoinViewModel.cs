using CoinCap.Clients;
using CoinCap.Entities.Assets;
using CoinGecko.Clients;
using CoinGecko.Entities.Coins;
using CryptoInfoApp.Core;
using CryptoInfoApp.Model;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Windows;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Linq;
using CoinCap.Entities;
using System.Text.Json;
using LiveChartsCore;
using System.Text;

namespace CryptoInfoApp.ViewModel
{
    public class CoinViewModel : ObservableObject, IDisposable
    {
        private CoinGeckoClient CoinGeckoApi { get; } = new();
        private CoinCapClient CoinCapApi { get; } = new();

        private CoinModel _coinModel;
        private string _coinId;

        private ObservableCollection<FinancialPoint> _financialPoints = new();

        private Dictionary<string, int> _rangesDictionary = new Dictionary<string, int>()
        {
            { "1d", 1 },
            { "7d", 7 },
            { "1m", 30 },
            { "3m", 90 },
            { "1y", 365 }
        };

        private List<(int range, long unitWidth, Func<double, string> labeler, List<FinancialPoint> financialPoints)> _chartCache = new();

        private Axis[] _xAxes;
        private ISeries[] _series;

        public Axis[] XAxes
        {
            get => _xAxes;
            set
            {
                _xAxes = value;
                OnPropertyChanged(nameof(XAxes));
            }
        }
        public ISeries[] Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        private Loader _chartLoader;
        public Loader ChartLoader
        {
            get => _chartLoader;
            set
            {
                _chartLoader = value;
                OnPropertyChanged(nameof(ChartLoader));
            }
        }

        public Loader Loader { get; set; }
        public CoinModel CoinModel
        {
            get => _coinModel;
            set
            {
                _coinModel = value;
                OnPropertyChanged(nameof(CoinModel));
            }
        }
        public RelayCommand<string> ChangeChartRange { get; set; }

        private bool TryGetFromChartCache(string range)
        {
            int daysRange = _rangesDictionary[range];

            if (_chartCache.Any(t => t.range == daysRange))
            {
                ChartLoader.LoadingSuccess();
                var cacheItem = _chartCache.Where(x => x.range == daysRange).First();

                XAxes = new Axis[1]
                {
                        new Axis
                        {
                            LabelsRotation = 0,
                            UnitWidth = cacheItem.unitWidth,
                            Labeler = cacheItem.labeler,
                            MinLimit = null,
                            MaxLimit = null
                        }
                };

                Series = new ISeries[1]
                {
                        new CandlesticksSeries<FinancialPoint>
                        {
                            MaxBarWidth = SystemParameters.WorkArea.Width / 15,
                            Values = cacheItem.financialPoints,
                            TooltipLabelFormatter = (ChartPoint) =>
                                                    $"Date: {ChartPoint.Model.Date.ToString("dd/MM/yyyy  HH:mm")}{Environment.NewLine}" +
                                                    $"High: {ChartPoint.Model.High}${Environment.NewLine}" +
                                                    $"Open: {ChartPoint.Model.Open}${Environment.NewLine}" +
                                                    $"Close: {ChartPoint.Model.Close}${Environment.NewLine}" +
                                                    $"Low: {ChartPoint.Model.Low}$"
                        }
                };

                return true;
            }
            return false;
        }

        private async Task FetchAndSetupChart(string range)
        {
            int daysRange = _rangesDictionary[range];
            ChartLoader = new Loader();
            try
            {
                var ohlcData = await CoinGeckoApi.CoinsClient.GetOhlcByCoinId(_coinId, "usd", daysRange.ToString());

                double[][] ohlcArray = JsonSerializer.Deserialize<double[][]>(ohlcData.ToString());

                if (ohlcArray != null)
                {
                    _financialPoints.Clear();
                    ConvertToFinancialPoint(ohlcArray).ForEach(x => _financialPoints.Add(x));

                    ChartLoader.LoadingSuccess();
                }
            }
            catch (HttpRequestException ex)
            {
                ChartLoader.CoinGeckoError(ex.Message);
            }
            finally
            {
                if (!ChartLoader.IsLoaded)
                {
                    ChartLoader.LoadingFailed("Failed to get chart data, please try again by changing the chart range" +
                                              "\nPlease note that the API has a limited number of requests per minute");
                }
                else
                {
                    long unitWidth = 0;
                    Func<double, string> labeler = new Func<double, string>(value => string.Empty);

                    if (daysRange <= 2)
                    {
                        unitWidth = TimeSpan.FromMinutes(30).Ticks;
                        labeler = value => new DateTime((long)value).ToString("HH:mm");
                    }
                    else if (daysRange > 2 && daysRange <= 30)
                    {
                        unitWidth = TimeSpan.FromHours(4).Ticks;
                        labeler = value => new DateTime((long)value).ToString($"HH:mm{Environment.NewLine}dd/MM");
                    }
                    else if (daysRange > 30)
                    {
                        unitWidth = TimeSpan.FromDays(4).Ticks;
                        labeler = value => new DateTime((long)value).ToString($"dd/MM{Environment.NewLine}yyyy");
                    }

                    if (_chartCache.Any(x => x.range == daysRange))
                    {
                        _chartCache[_chartCache.FindIndex(x => x.range == daysRange)] = (daysRange, unitWidth, labeler, _financialPoints.ToList());
                    }
                    else
                    {
                        _chartCache.Add((daysRange, unitWidth, labeler, _financialPoints.ToList()));
                    }

                    XAxes = new Axis[1]
                    {
                        new Axis
                        {
                            LabelsRotation = 0,
                            UnitWidth = unitWidth,
                            Labeler = labeler,
                            MinLimit = null,
                            MaxLimit = null
                        }
                    };

                    Series = new ISeries[1]
                    {
                        new CandlesticksSeries<FinancialPoint>
                        {
                            MaxBarWidth = SystemParameters.WorkArea.Width / 15,
                            Values = _financialPoints,
                            TooltipLabelFormatter = (ChartPoint) =>
                                                    $"Date: {ChartPoint.Model.Date.ToString("dd/MM/yyyy  HH:mm")}{Environment.NewLine}" +
                                                    $"High: {ChartPoint.Model.High}${Environment.NewLine}" +
                                                    $"Open: {ChartPoint.Model.Open}${Environment.NewLine}" +
                                                    $"Close: {ChartPoint.Model.Close}${Environment.NewLine}" +
                                                    $"Low: {ChartPoint.Model.Low}$"
                        }
                    };
                }
            }
        }

        private List<FinancialPoint> ConvertToFinancialPoint(double[][] marketChartById)
        {
            List<FinancialPoint> financialPoints = new List<FinancialPoint>();
            foreach (var c in marketChartById)
            {
                if (c != null)
                {
                    var point = new FinancialPoint();

                    point.Date = DateTimeOffset.FromUnixTimeMilliseconds((long)c[0]).UtcDateTime;
                    point.Open = c[1];
                    point.High = c[2];
                    point.Low = c[3];
                    point.Close = c[4];

                    financialPoints.Add(point);
                }
            }
            return financialPoints;
        }

        private static IEnumerable<string> StringToTextBlocks(string input)
        {
            var tagFound = false;
            StringBuilder sb = new();

            foreach (var t in input)
            {
                if (t == '<') tagFound = true;
                if (!tagFound) sb.Append(t);
                if (t == '>') tagFound = false;
            }

            return sb.ToString().Split("\r\n\r\n");
        }

        private CoinModel ConvertToCoinModel(CoinMarkets coinGeckoData, CoinFullDataById coinFullDataById)
        {
            var coinModel = new CoinModel
            {
                Id = coinGeckoData.Id,
                Rank = coinGeckoData.MarketCapRank,
                Image = coinGeckoData.Image,
                Name = coinGeckoData.Name,
                Symbol = coinGeckoData.Symbol,
                Price = coinGeckoData.CurrentPrice,
                PriceChangePercentage24H = coinGeckoData.PriceChangePercentage24HInCurrency,
                PriceChangePercentage7D = coinGeckoData.PriceChangePercentage7DInCurrency,
                MarketCap = coinGeckoData.MarketCap,
                Volume24H = coinGeckoData.TotalVolume,
                CirculatingSupply = coinGeckoData.CirculatingSupply,
                Description = new List<string>(),
                PriceChange24H = coinGeckoData.PriceChange24H,
                High24H = coinGeckoData.High24H,
                Low24H = coinGeckoData.Low24H,
                FullyDilutedMarketCap = coinGeckoData.FullyDilutedMarketCap,
                Ath = coinGeckoData.Ath,
                AthChangePercentage = coinGeckoData.AthChangePercentage,
                AthDate = coinGeckoData.AthDate,
                Atl = coinGeckoData.Atl,
                AtlChangePercentage = coinGeckoData.AtlChangePercentage,
                AtlDate = coinGeckoData.AtlDate,
            };
            StringToTextBlocks(coinFullDataById.Description["en"]).ToList().ForEach(x => coinModel.Description.Add(x));

            return coinModel;
        }

        private CoinModel ConvertToCoinModel(AssetById coinCapData)
        {

            var numberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };
            var coinModel = new CoinModel
            {
                Id = coinCapData.Id,
                Image = null,
                Name = coinCapData.Name,
                Symbol = coinCapData.Symbol,
                PriceChangePercentage7D = null,
                Description = null,
                PriceChange24H = null,
                High24H = null,
                Low24H = null,
                FullyDilutedMarketCap = null,
                Ath = null,
                AthChangePercentage = null,
                AthDate = null,
                Atl = null,
                AtlChangePercentage = null,
                AtlDate = null,
            };

            if (long.TryParse(coinCapData.Rank, out long rank))
                coinModel.Rank = rank;

            if (decimal.TryParse(coinCapData.PriceUsd, NumberStyles.Any, numberFormat, out decimal priceUsd))
                coinModel.Price = priceUsd;

            if (decimal.TryParse(coinCapData.ChangePercent24Hr, NumberStyles.Any, numberFormat, out decimal priceChangePercentage24H))
                coinModel.PriceChangePercentage24H = priceChangePercentage24H;

            if (decimal.TryParse(coinCapData.MarketCapUsd, NumberStyles.Any, numberFormat, out decimal marketCapUsd))
                coinModel.MarketCap = marketCapUsd;

            if (decimal.TryParse(coinCapData.VolumeUsd24Hr, NumberStyles.Any, numberFormat, out decimal volumeUsd24Hr))
                coinModel.Volume24H = volumeUsd24Hr;

            if (decimal.TryParse(coinCapData.Supply, NumberStyles.Any, numberFormat, out decimal circulatingSupply))
                coinModel.CirculatingSupply = circulatingSupply;


            return coinModel;
        }

        private Task<MarketChartById> GetChartFromCoingGeckoApi()
        {
            return CoinGeckoApi.CoinsClient.GetMarketChartRangeByCoinId(_coinId, "usd", null, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
        }

        private (Task<List<CoinMarkets>>, Task<CoinFullDataById>) GetFromCoinGeckoApi()
        {
            (Task<List<CoinMarkets>>, Task<CoinFullDataById>) result =
                (
                CoinGeckoApi.CoinsClient.GetCoinMarkets(vsCurrency: "usd", ids: new[] { _coinId }, order: "market_cap_desc", perPage: 1, page: 0, sparkline: true, priceChangePercentage: "24h,7d", category: ""),
                CoinGeckoApi.CoinsClient.GetAllCoinDataWithId(_coinId, "false", false, false, false, false, false)
                );

            return result;
        }

        private Task<ApiResponse<AssetById>> GetFromCoinCapApi()
        {
            return CoinCapApi.AssetsClient.GetAssetById(_coinId);
        }

        private async Task FetchData()
        {
            try
            {
                var tasks = GetFromCoinGeckoApi();
                var coinGeckoResponse = await tasks.Item1;
                var coinGeckoFullDataByIdResponse = await tasks.Item2;

                await Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    CoinModel = ConvertToCoinModel(coinGeckoResponse.First(), coinGeckoFullDataByIdResponse);
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
                        CoinModel = ConvertToCoinModel(coinCapResponse.Data);
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

        public CoinViewModel(string coinId)
        {
            _coinId = coinId;
            Loader = new Loader();
            ChartLoader = new Loader();

            ChangeChartRange = new RelayCommand<string>(range =>
            {
                if (!TryGetFromChartCache(range))
                    Task.Run(async () => await FetchAndSetupChart(range));
            });

            Task.Run(async () =>
            {
                await FetchData();
                if (CoinModel != null)
                    await FetchAndSetupChart("7d");
            });
        }

        public CoinViewModel() => Loader = new Loader("To view detailed information about a coin, select it from the main page or the search page", false);

        public void Dispose()
        {
            CoinGeckoApi.Dispose();
            CoinCapApi.Dispose();
        }
    }
}