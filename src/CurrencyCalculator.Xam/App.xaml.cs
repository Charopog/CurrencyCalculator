using Prism;
using Prism.Ioc;
using CurrencyCalculator.Xam.ViewModels;
using CurrencyCalculator.Xam.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using CurrencyCalculator.Xam.Services.Abstractions;
using CurrencyCalculator.Xam.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CurrencyCalculator.Xam
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("CalculatorPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ICalculatorService, CalculatorService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CalculatorPage>();
            containerRegistry.RegisterForNavigation<CurrencyConvertPage>();
        }
    }
}
