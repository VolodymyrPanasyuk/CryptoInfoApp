using CoinGecko.ApiEndPoints;

namespace CoinCap.ApiEndPosints
{
    public static class CandlesEndPoints
    {
        public static readonly string Candles = "candles";
        public static string AllCandles() => BaseApiEndPointUrl.ApiEndPoint + Candles;
    }
}