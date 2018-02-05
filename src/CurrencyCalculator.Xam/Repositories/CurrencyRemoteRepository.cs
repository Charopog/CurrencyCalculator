using CurrencyCalculator.Xam.Repositories.Abstractions;
using CurrencyCalculator.Xam.Utils.Factories.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculator.Xam.Repositories
{
    public class CurrencyRemoteRepository : ICurrencyRemoteRepository
    {
        private readonly HttpClient _httpClient;

        public CurrencyRemoteRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.GetOrCreateHttpClient("https://api.fixer.io/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetLatestRatesAsync()
        {
            var response = await _httpClient.GetAsync("latest").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var jsonRates = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return jsonRates;
        }
    }
}
