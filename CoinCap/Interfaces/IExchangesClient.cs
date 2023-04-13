using CoinCap.Entities;
using CoinCap.Entities.Exchanges;

namespace CoinCap.Interfaces
{
    public interface IExchangesClient
    {
        Task<ApiResponseArray<ExchangeById>> GetExchanges();
        Task<ApiResponse<ExchangeById>> ExchangeById(string id);
    }
}