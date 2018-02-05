using CurrencyCalculator.Xam.Utils.Factories.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CurrencyCalculator.Xam.Utils.Factories
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private IDictionary<string, HttpClient> _cachedClients;

        public HttpClientFactory()
        {
            _cachedClients = new Dictionary<string, HttpClient>();
        }

        public HttpClient GetOrCreateHttpClient(string baseAddress)
        {
            if (_cachedClients.TryGetValue(baseAddress, out var existingClient))
            {
                return existingClient;
            }

            var newClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

            _cachedClients.Add(baseAddress, newClient);

            return newClient;

        }
        public void DisposeClient(string baseAddress)
        {
            if (_cachedClients.TryGetValue(baseAddress, out var existingClient))
            {
                existingClient.Dispose();
                _cachedClients.Remove(baseAddress);
            }
            else
            {
                throw new InvalidOperationException($"Client with Base Address {baseAddress} does not exist");
            }
        }
        public void DisposeAllClients()
        {
            foreach (KeyValuePair<string, HttpClient> entry in _cachedClients)
            {
                entry.Value.Dispose();
            }
            _cachedClients.Clear();
        }


    }
}
