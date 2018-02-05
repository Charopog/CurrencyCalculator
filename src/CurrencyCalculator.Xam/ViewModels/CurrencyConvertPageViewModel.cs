using CurrencyCalculator.Xam.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyCalculator.Xam.ViewModels
{
	public class CurrencyConvertPageViewModel : ViewModelBase
    {
        public CurrencyConvertPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SourceCurrency = Currency.BGN;
        }

        private Currency _sourceCurrency;
        public Currency SourceCurrency
        {
            get { return _sourceCurrency; }
            set { SetProperty(ref _sourceCurrency, value); }
        }
    }
}
