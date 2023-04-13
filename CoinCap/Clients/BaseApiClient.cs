namespace CoinCap.Clients;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CoinCap.Interfaces;

public class BaseApiClient : IAsyncApiRepository
{
    private readonly HttpClient _httpClient;

    public BaseApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<T> GetAsync<T>(Uri resourceUri)
    {
        var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, resourceUri))
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            return JsonSerializer.Deserialize<T>(responseContent);
        }
        catch (Exception e)
        {
            throw new HttpRequestException(e.Message);
        }
    }
}