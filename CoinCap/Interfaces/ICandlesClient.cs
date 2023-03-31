using CoinCap.Entities.Candles;

namespace CoinCap.Interfaces
{
    public interface ICandlesClient
    {
        Task<List<Candle>> GetCandles(string baseId, string quoteId, string interval);
        Task<List<Candle>> GetCandles(string? exchange, string? interval, string? baseId, string? quoteId, long? start, long? end);
    }
}