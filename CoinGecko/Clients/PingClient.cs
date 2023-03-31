namespace CoinGecko.Clients;

using System.Net.Http;
using System.Threading.Tasks;
using CoinGecko.ApiEndPoints;
using CoinGecko.Entities;
using CoinGecko.Interfaces;
using CoinGecko.Services;

public class PingClient : BaseApiClient, IPingClient
{
    public PingClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Ping> GetPingAsync()
    {
        return await GetAsync<Ping>(QueryStringService.AppendQueryString(PingApiEndPoint.PingApi())).ConfigureAwait(false);
    }
}