namespace CoinGecko.Interfaces;

using System.Threading.Tasks;
using CoinGecko.Entities.Response.Simple;

public interface ISimpleClient
{
    Task<Price> GetSimplePrice(string[] ids, string[] vsCurrencies);

    Task<Price> GetSimplePrice(string[] ids, string[] vsCurrencies, bool includeMarketCap,
        bool include24HVol, bool include24HChange, bool includeLastUpdatedAt);

    Task<TokenPrice> GetTokenPrice(string id, string[] contractAddress, string[] vsCurrencies);

    Task<TokenPrice> GetTokenPrice(string id, string[] contractAddress, string[] vsCurrencies,
        bool includeMarketCap, bool include24HVol, bool include24HChange, bool includeLastUpdatedAt);

    Task<SupportedCurrencies> GetSupportedVsCurrencies();
}