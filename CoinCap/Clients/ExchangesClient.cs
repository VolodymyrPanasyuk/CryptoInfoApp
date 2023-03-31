using CoinCap.ApiEndPosints;
using CoinCap.Entities.Exchanges;
using CoinCap.Interfaces;
using CoinGecko.Clients;
using CoinGecko.Services;

namespace CoinCap.Clients
{
    public class ExchangesClient : BaseApiClient, IExchangesClient
    {
        public ExchangesClient(HttpClient client) : base(client) { }

        public async Task<ExchangeById> ExchangeById(string id)
        {
            return await GetAsync<ExchangeById>(QueryStringService.AppendQueryString(ExchangesEndPoints.ExchangeById(id))).ConfigureAwait(false);
        }

        public Task<List<ExchangeById>> GetExchanges()
        {
            return GetAsync<List<ExchangeById>>(QueryStringService.AppendQueryString(ExchangesEndPoints.AllExchanges()));
        }
    }
}