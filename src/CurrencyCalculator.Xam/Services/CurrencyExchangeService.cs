using CurrencyCalculator.Xam.Constants;
using CurrencyCalculator.Xam.Model;
using CurrencyCalculator.Xam.Repositories.Abstractions;
using CurrencyCalculator.Xam.Services.Abstractions;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CurrencyCalculator.Xam.Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly ICurrencyRemoteRepository _currencyRemoteRepository;
        private readonly ICurrencyLocalRepository _currencyLocalRepository;

        private ExchangeRates _exchangeRates;

        public CurrencyExchangeService(ICurrencyRemoteRepository currencyRemoteRepository, ICurrencyLocalRepository currencyLocalRepository)
        {
            _currencyRemoteRepository = currencyRemoteRepository 
                ?? throw new ArgumentNullException(nameof(currencyRemoteRepository));
            _currencyLocalRepository = currencyLocalRepository
                ?? throw new ArgumentNullException(nameof(currencyLocalRepository));
        }

        public async Task GetLatestExchangeRates()
        {
            if(!_currencyLocalRepository.ContainsRatings())
            {
                _currencyLocalRepository.LoadDefault();
            }

            var localJsonRates = _currencyLocalRepository.GetJsonRates();
            _exchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(localJsonRates);
            try
            {
                var remoteJsonRates = await _currencyRemoteRepository.GetLatestRatesAsync();
                _currencyLocalRepository.AddOrUpdateRates(remoteJsonRates);
                _exchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(localJsonRates);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }

        public double ConvertCurrency(Currency sourceCurrency, Currency targetCurrency, double value)
        {
            if(sourceCurrency == targetCurrency)
            {
                return value;
            }
            if(sourceCurrency == Currency.EUR)
            {
                _exchangeRates.Rates.TryGetValue(targetCurrency.ToString(), out var stringRate);
                var rate = Double.Parse(stringRate);
                return Math.Round(value * rate, 2);
            }
            if (targetCurrency == Currency.EUR)
            {
                _exchangeRates.Rates.TryGetValue(sourceCurrency.ToString(), out var stringRate);
                var rate = Double.Parse(stringRate);
                return Math.Round(value / rate, 2);
            }
            _exchangeRates.Rates.TryGetValue(sourceCurrency.ToString(), out var sourceCrossRate);
            _exchangeRates.Rates.TryGetValue(targetCurrency.ToString(), out var targetCrossRate);

            var crossRate =  Double.Parse(targetCrossRate) / Double.Parse(sourceCrossRate);

            return Math.Round(value * crossRate, 2);

        }
    }
}
