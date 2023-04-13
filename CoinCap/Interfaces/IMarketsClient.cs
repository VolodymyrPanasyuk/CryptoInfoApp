using CoinCap.Entities;
using CoinCap.Entities.Markets;

namespace CoinCap.Interfaces
{
    public interface IMarketsClient
    {
        Task<ApiResponseArray<Market>> GetMarkets(string? exchangeId, string? baseSymbol, string? quoteSymbol, string? baseId, string? quoteId, string? assetSymbol, string? assetId, int? limit, int? offset);
    }
}