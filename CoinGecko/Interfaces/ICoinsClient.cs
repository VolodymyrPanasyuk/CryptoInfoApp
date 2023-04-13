namespace CoinGecko.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using CoinGecko.Entities.Coins;

public interface ICoinsClient
{
    Task<IReadOnlyList<CoinFullData>> GetAllCoinsData();

    Task<IReadOnlyList<CoinFullData>> GetAllCoinsData(string order, int? perPage, int? page, string localization,
        bool? sparkline);

    Task<IReadOnlyList<CoinList>> GetCoinList();

    Task<IReadOnlyList<CoinList>> GetCoinList(bool includePlatform);

    Task<List<CoinMarkets>> GetCoinMarkets(string vsCurrency);

    Task<List<CoinMarkets>> GetCoinMarkets(string vsCurrency, string[] ids, string order, int? perPage, int? page,
        bool sparkline, string priceChangePercentage, string category);

    Task<CoinFullDataById> GetAllCoinDataWithId(string id);

    Task<CoinFullDataById> GetAllCoinDataWithId(string id, string localization, bool tickers,
        bool marketData, bool communityData, bool developerData, bool sparkline);

    Task<TickerById> GetTickerByCoinId(string id);


    Task<TickerById> GetTickerByCoinId(string id, int? page);

    Task<TickerById> GetTickerByCoinId(string id, string[] exchangeIds, int? page);

    Task<TickerById> GetTickerByCoinId(string id, string[] exchangeIds, int? page, string includeExchangeLogo,
        string order, bool depth);

    Task<CoinFullData> GetHistoryByCoinId(string id, string date, string localization);

    Task<MarketChartById> GetMarketChartsByCoinId(string id, string vsCurrency, string days);

    Task<MarketChartById> GetMarketChartsByCoinId(string id, string vsCurrency, string days, string interval);

    Task<MarketChartById> GetMarketChartRangeByCoinId(string id, string vsCurrency, string from, string to);

    Task<dynamic> GetOhlcByCoinId(string id, string vsCurrency, string days);
}