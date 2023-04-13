using CoinCap.ApiEndPosints;
using CoinCap.Entities;
using CoinCap.Entities.Rates;
using CoinCap.Interfaces;
using CoinCap.Services;

namespace CoinCap.Clients
{
    public class RatesClient : BaseApiClient, IRatesClient
    {
        public RatesClient(HttpClient client) : base(client) { }

        public async Task<ApiResponse<RateById>> GetRateById(string id)
        {
            return await GetAsync<ApiResponse<RateById>>(QueryStringService.AppendQueryString(RatesEndPoints.RateById(id))).ConfigureAwait(false);
        }

        public async Task<ApiResponseArray<RateById>> GetRates()
        {
            return await GetAsync<ApiResponseArray<RateById>>(QueryStringService.AppendQueryString(RatesEndPoints.AllRates())).ConfigureAwait(false);
        }
    }
}