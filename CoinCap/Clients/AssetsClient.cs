using CoinCap.ApiEndPosints;
using CoinCap.Entities;
using CoinCap.Entities.Assets;
using CoinCap.Interfaces;
using CoinCap.Parameters;
using CoinCap.Services;

namespace CoinCap.Clients
{
    public class AssetsClient : BaseApiClient, IAssetsClient
    {
        public AssetsClient(HttpClient client) : base(client) { }

        public async Task<ApiResponseArray<AssetById>> GetAssets(int limit, int offset)
        {
            return await GetAssets(null, null, limit, offset).ConfigureAwait(false);
        }

        public async Task<ApiResponseArray<AssetById>> GetAssets(string? search, string[]? ids, int? limit, int? offset)
        {
            return await GetAsync<ApiResponseArray<AssetById>>(QueryStringService.AppendQueryString(AssetsEndPoints.AllAssets(),
                new Dictionary<string, object>
                {
                    {"search", search},
                    {"ids", ids == null? "": string.Join(",", ids)},
                    {"limit", limit},
                    {"offset", offset}
                })).ConfigureAwait(false);
        }

        public async Task<ApiResponse<AssetById>> GetAssetById(string id)
        {
            return await GetAsync<ApiResponse<AssetById>>(QueryStringService.AppendQueryString(AssetsEndPoints.AssetById(id))).ConfigureAwait(false);
        }

        public async Task<ApiResponseArray<HistoryById>> GetHistoryById(string id)
        {
            return await GetHistoryById(id, HistoryInterval.OneDay, null, null).ConfigureAwait(false);
        }

        public async Task<ApiResponseArray<HistoryById>> GetHistoryById(string id, string interval, long? start, long? end)
        {
            return await GetAsync<ApiResponseArray<HistoryById>>(QueryStringService.AppendQueryString(AssetsEndPoints.HistoryById(id),
                new Dictionary<string, object>
                {
                    {"interval", interval},
                    {"start", start},
                    {"end", end}
                })).ConfigureAwait(false);
        }

        public Task<ApiResponseArray<MarketById>> GetMarketsById(string id, int? limit, int? offset)
        {
            return GetAsync<ApiResponseArray<MarketById>>(QueryStringService.AppendQueryString(AssetsEndPoints.MarketsById(id),
                new Dictionary<string, object>
                {
                    {"limit", limit},
                    {"offset", offset}
                }));
        }
    }
}