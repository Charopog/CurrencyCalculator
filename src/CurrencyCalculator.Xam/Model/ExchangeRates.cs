using CurrencyCalculator.Xam.Utils.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyCalculator.Xam.Model
{
    public class ExchangeRates
    {
        [JsonProperty("base")]
        public string BaseCurrency { get; set; }

        [JsonProperty("date")]
        public System.DateTime Date { get; set; }

        [JsonProperty("rates")]
        [JsonConverter(typeof(DictionaryConverter))]
        public Dictionary<string, string> Rates;
    }
}
