namespace CoinCap.Interfaces;

using System;
using System.Threading.Tasks;

public interface IAsyncApiRepository
{
    Task<T> GetAsync<T>(Uri resourceUri);
}