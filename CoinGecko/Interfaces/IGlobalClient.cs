namespace CoinGecko.Interfaces;

using System.Threading.Tasks;
using CoinGecko.Entities.Global;

public interface IGlobalClient
{
    Task<Global> GetGlobal();
    Task<GlobalDeFi> GetGlobalDeFi();
}