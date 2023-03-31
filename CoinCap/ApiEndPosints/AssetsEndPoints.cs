using CoinGecko.ApiEndPoints;

namespace CoinCap.ApiEndPosints
{
    public static class AssetsEndPoints
    {
        public static readonly string Assets = "assets";
        public static string AllAssets() => BaseApiEndPointUrl.ApiEndPoint + Assets;
        public static string AssetById(string id) => BaseApiEndPointUrl.ApiEndPoint + Assets + '/' + id;
        public static string HistoryById(string id) => AssetById(id) + "/history";
        public static string MarketsById(string id) => AssetById(id) + "/markets";
    }
}