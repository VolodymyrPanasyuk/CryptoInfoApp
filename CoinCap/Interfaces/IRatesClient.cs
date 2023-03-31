using CoinCap.Entities.Rates;

namespace CoinCap.Interfaces
{
    public interface IRatesClient
    {
        Task<List<RateById>> GetRates();

        Task<RateById> GetRateById(string id);
    }
}