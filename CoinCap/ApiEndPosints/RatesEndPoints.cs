using CoinCap.ApiEndPoints;

namespace CoinCap.ApiEndPosints
{
    public static class RatesEndPoints
    {
        public static readonly string Rates = "rates";
        public static string AllRates() => BaseApiEndPointUrl.ApiEndPoint + Rates;
        public static string RateById(string id) => BaseApiEndPointUrl.ApiEndPoint + Rates + '/' + id;
    }
}