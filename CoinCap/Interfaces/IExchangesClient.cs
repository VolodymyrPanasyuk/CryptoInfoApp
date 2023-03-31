using CoinCap.Entities.Exchanges;

namespace CoinCap.Interfaces
{
    public interface IExchangesClient
    {
        Task<List<ExchangeById>> GetExchanges();

        Task<ExchangeById> ExchangeById(string id);
    }
}