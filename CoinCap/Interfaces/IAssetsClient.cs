using CoinCap.Entities.Assets;

namespace CoinCap.Interfaces
{
    public interface IAssetsClient
    {
        Task<List<AssetById>> GetAllAssets(int limit);
        Task<List<AssetById>> GetAllAssets(string? search, string[]? ids, int? limit, int? offset);

        Task<AssetById> GetAssetById(string id);

        Task<List<HistoryById>> GetHistoryById(string id);

        Task<List<HistoryById>> GetHistoryById(string id, string interval, long? start, long? end);

        Task<List<MarketById>> GetMarketsById(string id, int? limit, int? offset);
    }
}