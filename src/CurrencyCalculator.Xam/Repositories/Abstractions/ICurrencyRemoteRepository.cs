using System.Threading.Tasks;

namespace CurrencyCalculator.Xam.Repositories.Abstractions
{
    public interface ICurrencyRemoteRepository
    {
        Task<string> GetLatestRatesAsync();
    }
}