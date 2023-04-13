using CoinCap.Entities;
using CoinCap.Entities.Assets;

namespace CoinCap.Interfaces
{
    public interface IAssetsClient
    {
        Task<ApiResponseArray<AssetById>> GetAssets(int limitm, int offset);
        Task<ApiResponseArray<AssetById>> GetAssets(string? search, string[]? ids, int? limit, int? offset);
        Task<ApiResponse<AssetById>> GetAssetById(string id);
        Task<ApiResponseArray<HistoryById>> GetHistoryById(string id);
        Task<ApiResponseArray<HistoryById>> GetHistoryById(string id, string interval, long? start, long? end);
        Task<ApiResponseArray<MarketById>> GetMarketsById(string id, int? limit, int? offset);
    }
}