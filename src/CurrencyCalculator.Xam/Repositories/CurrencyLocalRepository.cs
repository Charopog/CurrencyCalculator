using CurrencyCalculator.Xam.Repositories.Abstractions;
using Plugin.Settings.Abstractions;
using System;

namespace CurrencyCalculator.Xam.Repositories
{
    public class CurrencyLocalRepository : ICurrencyLocalRepository
    {
        private const string Rates = "rates";

        private readonly ISettings _settings;

        public CurrencyLocalRepository(ISettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void AddOrUpdateRates(string jsonRates)
        {
            _settings.AddOrUpdateValue(Rates, jsonRates);
        }

        public bool ContainsRatings()
        {
            return _settings.Contains(Rates);
        }
        public string GetJsonRates()
        {
            return _settings.GetValueOrDefault(Rates, null);
        }
        public void LoadDefault()
        {
            string jsonDefault = @"{ ""base"":""EUR"",
                                    ""date"":""2018-02-05"",
                                    ""rates"":{ ""AUD"":1.5664,""BGN"":1.9558,""BRL"":4.0174,
                                                ""CAD"":1.5468,""CHF"":1.1599,""CNY"":7.8248,
                                                ""CZK"":25.196,""DKK"":7.4433,""GBP"":0.88568,
                                                ""HKD"":9.7288,""HRK"":7.433,""HUF"":309.6,
                                                ""IDR"":16788.0,""ILS"":4.2812,""INR"":79.744,
                                                ""ISK"":125.0,""JPY"":136.67,""KRW"":1352.6,
                                                ""MXN"":23.124,""MYR"":4.8557,""NOK"":9.6108,
                                                ""NZD"":1.7025,""PHP"":64.169,""PLN"":4.1585,
                                                ""RON"":4.6333,""RUB"":70.498,""SEK"":9.8338,
                                                ""SGD"":1.6387,""THB"":39.223,""TRY"":4.6774,
                                                ""USD"":1.244,""ZAR"":14.964} }";
            AddOrUpdateRates(jsonDefault);
        }
    }
}
