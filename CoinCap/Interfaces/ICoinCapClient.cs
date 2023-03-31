namespace CoinCap.Interfaces
{
    public interface ICoinCapClient
    {
        IAssetsClient AssetsClient { get; }
        IRatesClient RatesClient { get; }
        IExchangesClient ExchangesClient { get; }
        IMarketsClient MarketsClient { get; }
        ICandlesClient CandlesClient { get; }
    }
}