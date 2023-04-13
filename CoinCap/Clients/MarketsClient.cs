using CoinCap.ApiEndPosints;
using CoinCap.Entities;
using CoinCap.Entities.Markets;
using CoinCap.Interfaces;
using CoinCap.Services;

namespace CoinCap.Clients
{
    public class MarketsClient : BaseApiClient, IMarketsClient
    {
        public MarketsClient(HttpClient client) : base(client) { }

        public async Task<ApiResponseArray<Market>> GetMarkets(string? exchangeId, string? baseSymbol, string? quoteSymbol, string? baseId, string? quoteId, string? assetSymbol, string? assetId, int? limit, int? offset)
        {
            return await GetAsync<ApiResponseArray<Market>>(QueryStringService.AppendQueryString(MarketsEndPoints.AllMarkets(),
                new Dictionary<string, object>
                {
                    {"exchangeId", exchangeId},
                    {"baseSymbol", baseSymbol},
                    {"quoteSymbol", quoteSymbol},
                    {"baseId", baseId},
                    {"quoteId", quoteId},
                    {"assetSymbol", assetSymbol},
                    {"assetId", assetId},
                    {"limit", limit},
                    {"offset", offset}
                })).ConfigureAwait(false);
        }
    }
}