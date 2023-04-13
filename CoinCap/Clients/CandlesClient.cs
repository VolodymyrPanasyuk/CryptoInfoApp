using CoinCap.ApiEndPosints;
using CoinCap.Entities;
using CoinCap.Entities.Candles;
using CoinCap.Interfaces;
using CoinCap.Parameters;
using CoinCap.Services;

namespace CoinCap.Clients
{
    public class CandlesClient : BaseApiClient, ICandlesClient
    {
        public CandlesClient(HttpClient client) : base(client) { }

        public async Task<ApiResponseArray<Candle>> GetCandles(string exchange, string interval, string baseId, string quoteId, long? start, long? end)
        {
            return await GetAsync<ApiResponseArray<Candle>>(QueryStringService.AppendQueryString(CandlesEndPoints.AllCandles(),
                new Dictionary<string, object>
                {
                    {"exchange", exchange},
                    {"interval", interval},
                    {"baseId", baseId},
                    {"quoteId", quoteId},
                    {"start", start},
                    {"end", end}
                })).ConfigureAwait(false);
        }

        public async Task<ApiResponseArray<Candle>> GetCandles(string baseId, string quoteId, string? interval)
        {
            return await GetCandles("poloniex", interval != null ? interval : CandlesInterval.OneDay, baseId, quoteId, null, null).ConfigureAwait(false);
        }
    }
}