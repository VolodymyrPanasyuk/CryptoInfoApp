namespace CoinGecko.Interfaces;

using System.Threading.Tasks;
using CoinGecko.Entities;
public interface IPingClient
{
    Task<Ping> GetPingAsync();
}