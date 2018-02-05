using CurrencyCalculator.Xam.Constants;
using System.Threading.Tasks;

namespace CurrencyCalculator.Xam.Services.Abstractions
{
    public interface ICurrencyExchangeService
    {
        Task GetLatestExchangeRates();
        double ConvertCurrency(Currency sourceCurrency, Currency targetCurrency, double value);
    }
}