using Prism;
using Prism.Ioc;
using CurrencyCalculator.Xam.ViewModels;
using CurrencyCalculator.Xam.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using CurrencyCalculator.Xam.Services.Abstractions;
using CurrencyCalculator.Xam.Services;
using CurrencyCalculator.Xam.Utils.Factories.Abstractions;
using CurrencyCalculator.Xam.Utils.Factories;
using CurrencyCalculator.Xam.Repositories.Abstractions;
using CurrencyCalculator.Xam.Repositories;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CurrencyCalculator.Xam
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("CalculatorPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICalculatorService, CalculatorService>();
            containerRegistry.RegisterSingleton<ICurrencyExchangeService, CurrencyExchangeService>();
            containerRegistry.RegisterSingleton<IHttpClientFactory, HttpClientFactory>();
            containerRegistry.Register<ICurrencyRemoteRepository, CurrencyRemoteRepository>();
            containerRegistry.Register<ICurrencyLocalRepository, CurrencyLocalRepository>();
            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CalculatorPage>();
            containerRegistry.RegisterForNavigation<CurrencyConvertPage>();
        }
    }
}
