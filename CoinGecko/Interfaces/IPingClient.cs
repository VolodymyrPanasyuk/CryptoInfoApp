namespace CoinGecko.Interfaces;

using System.Threading.Tasks;
using CoinGecko.Entities.Response;
public interface IPingClient
{
    Task<Ping> GetPingAsync();
}