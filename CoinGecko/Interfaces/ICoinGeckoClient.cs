namespace CoinGecko.Interfaces;
public interface ICoinGeckoClient
{
    ICoinsClient CoinsClient { get; }
    ISimpleClient SimpleClient { get; }
    IPingClient PingClient { get; }
    IGlobalClient GlobalClient { get; }
}