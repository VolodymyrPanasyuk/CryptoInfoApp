using CoinGecko.ApiEndPoints;

namespace CoinCap.ApiEndPosints
{
    public static class MarketsEndPoints
    {
        public static readonly string Markets = "markets";
        public static string AllMarkets() => BaseApiEndPointUrl.ApiEndPoint + Markets;
    }
}