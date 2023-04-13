using System;
using System.Net.Http;
using CoinGecko.Interfaces;

namespace CoinGecko.Clients;

public partial class CoinGeckoClient : IDisposable, ICoinGeckoClient
{
    private static readonly Lazy<CoinGeckoClient> Lazy = new(() => new CoinGeckoClient());

    private readonly HttpClient _httpClient;
    private bool _isDisposed;

    public CoinGeckoClient()
    {
        _httpClient = new();

        //Little hack, beacause CoinGecko API doesn't allow requests without User-Agent header
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36");
    }

    public static CoinGeckoClient Instance => Lazy.Value;

    public ISimpleClient SimpleClient => new SimpleClient(_httpClient);
    public IPingClient PingClient => new PingClient(_httpClient);
    public ICoinsClient CoinsClient => new CoinsClient(_httpClient);
    public IGlobalClient GlobalClient => new GlobalClient(_httpClient);


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }
        if (disposing)
        {
            _httpClient?.Dispose();
        }
        _isDisposed = true;
    }
}