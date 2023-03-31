using CoinCap.ApiEndPosints;
using CoinCap.Entities.Assets;
using CoinCap.Interfaces;
using CoinCap.Parameters;
using CoinGecko.Clients;
using CoinGecko.Services;

namespace CoinCap.Clients
{
    public class AssetsClient : BaseApiClient, IAssetsClient
    {
        public AssetsClient(HttpClient client) : base(client) { }

        public async Task<List<AssetById>> GetAllAssets(int limit)
        {
            return await GetAllAssets(null, null, limit, null).ConfigureAwait(false);
        }

        public async Task<List<AssetById>> GetAllAssets(string? search, string[]? ids, int? limit, int? offset)
        {
            return await GetAsync<List<AssetById>>(QueryStringService.AppendQueryString(AssetsEndPoints.AllAssets(),
                new Dictionary<string, object>
                {
                    {"search", string.Join(",", ids)},
                    {"ids", ids},
                    {"limit", limit},
                    {"offset", offset}
                })).ConfigureAwait(false);
        }

        public async Task<AssetById> GetAssetsById(string id)
        {
            return await GetAsync<AssetById>(QueryStringService.AppendQueryString(AssetsEndPoints.AssetById(id))).ConfigureAwait(false);
        }

        public async Task<List<HistoryById>> GetHistoryById(string id)
        {
            return await GetHistoryById(id, HistoryInterval.OneDay, null, null).ConfigureAwait(false);
        }

        public async Task<List<HistoryById>> GetHistoryById(string id, string interval, long? start, long? end)
        {
            return await GetAsync<List<HistoryById>>(QueryStringService.AppendQueryString(AssetsEndPoints.HistoryById(id),
                new Dictionary<string, object>
                {
                    {"interval", interval},
                    {"start", start},
                    {"end", end}
                })).ConfigureAwait(false);
        }

        public Task<List<MarketById>> GetMarketsById(string id, int? limit, int? offset)
        {
            return GetAsync<List<MarketById>>(QueryStringService.AppendQueryString(AssetsEndPoints.MarketsById(id),
                new Dictionary<string, object>
                {
                    {"limit", limit},
                    {"offset", offset}
                }));
        }
    }
}