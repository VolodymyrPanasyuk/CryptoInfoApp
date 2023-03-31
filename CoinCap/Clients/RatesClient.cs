using CoinCap.ApiEndPosints;
using CoinCap.Entities.Rates;
using CoinCap.Interfaces;
using CoinGecko.Clients;
using CoinGecko.Services;

namespace CoinCap.Clients
{
    public class RatesClient : BaseApiClient, IRatesClient
    {
        public RatesClient(HttpClient client) : base(client) { }

        public async Task<RateById> GetRateById(string id)
        {
            return await GetAsync<RateById>(QueryStringService.AppendQueryString(RatesEndPoints.RateById(id))).ConfigureAwait(false);
        }

        public async Task<List<RateById>> GetRates()
        {
            return await GetAsync<List<RateById>>(QueryStringService.AppendQueryString(RatesEndPoints.AllRates())).ConfigureAwait(false);
        }
    }
}