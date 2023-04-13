using CoinCap.Entities;
using CoinCap.Entities.Rates;

namespace CoinCap.Interfaces
{
    public interface IRatesClient
    {
        Task<ApiResponseArray<RateById>> GetRates();
        Task<ApiResponse<RateById>> GetRateById(string id);
    }
}