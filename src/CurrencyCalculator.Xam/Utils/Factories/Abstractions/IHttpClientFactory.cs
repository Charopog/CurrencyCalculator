using System.Net.Http;

namespace CurrencyCalculator.Xam.Utils.Factories.Abstractions
{
    public interface IHttpClientFactory
    {
        void DisposeAllClients();
        void DisposeClient(string baseAddress);
        HttpClient GetOrCreateHttpClient(string baseAddress);
    }
}