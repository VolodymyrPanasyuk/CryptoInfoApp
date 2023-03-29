namespace CoinGecko.ApiEndPoints
{
    public static class PingApiEndPoint
    {
        public static readonly string Ping = "ping";
        public static string PingApi() => BaseApiEndPointUrl.ApiEndPoint + Ping;
    }
}