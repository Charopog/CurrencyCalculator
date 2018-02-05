namespace CurrencyCalculator.Xam.Repositories.Abstractions
{
    public interface ICurrencyLocalRepository
    {
        void AddOrUpdateRates(string jsonRates);
        bool ContainsRatings();
        void LoadDefault();
        string GetJsonRates();
    }
}