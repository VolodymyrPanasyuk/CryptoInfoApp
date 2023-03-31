using CoinGecko.ApiEndPoints;

namespace CoinCap.ApiEndPosints
{
    public static class ExchangesEndPoints
    {
        public static readonly string Exchanges = "exchanges";
        public static string AllExchanges() => BaseApiEndPointUrl.ApiEndPoint + Exchanges;
        public static string ExchangeById(string id) => BaseApiEndPointUrl.ApiEndPoint + Exchanges + '/' + id;
    }
}