using CoinCap.ApiEndPosints;
using CoinCap.Entities;
using CoinCap.Entities.Exchanges;
using CoinCap.Interfaces;
using CoinCap.Services;

namespace CoinCap.Clients
{
    public class ExchangesClient : BaseApiClient, IExchangesClient
    {
        public ExchangesClient(HttpClient client) : base(client) { }

        public async Task<ApiResponse<ExchangeById>> ExchangeById(string id)
        {
            return await GetAsync<ApiResponse<ExchangeById>>(QueryStringService.AppendQueryString(ExchangesEndPoints.ExchangeById(id))).ConfigureAwait(false);
        }

        public Task<ApiResponseArray<ExchangeById>> GetExchanges()
        {
            return GetAsync<ApiResponseArray<ExchangeById>>(QueryStringService.AppendQueryString(ExchangesEndPoints.AllExchanges()));
        }
    }
}