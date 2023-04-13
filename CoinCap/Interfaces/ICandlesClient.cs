using CoinCap.Entities;
using CoinCap.Entities.Candles;

namespace CoinCap.Interfaces
{
    public interface ICandlesClient
    {
        Task<ApiResponseArray<Candle>> GetCandles(string baseId, string quoteId, string interval);
        Task<ApiResponseArray<Candle>> GetCandles(string? exchange, string? interval, string? baseId, string? quoteId, long? start, long? end);
    }
}