using CurrencyCalculator.Xam.Constants;
using CurrencyCalculator.Xam.Services.Abstractions;
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
        private readonly ICurrencyExchangeService _currencyExchangeService;
        private double _convertedCurrencyValue;


        public CurrencyConvertPageViewModel(INavigationService navigationService, ICurrencyExchangeService currencyExchangeService) 
            : base(navigationService)
        {
            _currencyExchangeService = currencyExchangeService 
                ?? throw new ArgumentNullException(nameof(currencyExchangeService));

            SourceCurrency = Currency.EUR;
            TargetCurrency = Currency.USD;

            DigitEntryCommand = new DelegateCommand<string>(HandleDigitEntry);
            PointEntryCommand = new DelegateCommand(HandlePointEntry);
            DeleteCommand = new DelegateCommand(DeleteLastEntry);
            ClearCommand = new DelegateCommand(ClearAll);
            EqualsCommand = new DelegateCommand(ExecuteEquals);
            NavigateToCalculatorCommand = new DelegateCommand(NavigateToCalculator);
        }


        public DelegateCommand<string> DigitEntryCommand { get; private set; }
        public DelegateCommand PointEntryCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }
        public DelegateCommand EqualsCommand { get; private set; }
        public DelegateCommand NavigateToCalculatorCommand { get; private set; }

        private Currency _sourceCurrency;
        public Currency SourceCurrency
        {
            get { return _sourceCurrency; }
            set
            {
                SetProperty(ref _sourceCurrency, value);
                ExecuteEquals();
            }
        }

        private Currency _targetCurrency;
        public Currency TargetCurrency
        {
            get { return _targetCurrency; }
            set
            {
                SetProperty(ref _targetCurrency, value);
                ExecuteEquals();
            }
        }

        private string _sourceCurrencyDisplayValue;
        public string SourceCurrencyDisplayValue
        {
            get { return _sourceCurrencyDisplayValue; }
            set
            {
                SetProperty(ref _sourceCurrencyDisplayValue, value);
                ExecuteEquals();
            }
        }

        public string TargetCurrencyDisplayValue
        {
            get
            {
                return $"{_convertedCurrencyValue}";
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if(parameters!=null)
            {
                SourceCurrencyDisplayValue = parameters.GetValue<string>("resultValue");
            }
            
        }

        private async void NavigateToCalculator()
        {
            await NavigationService.GoBackAsync(null, null, false);
        }

        private void ExecuteEquals()
        {
            if(String.IsNullOrWhiteSpace(_sourceCurrencyDisplayValue))
            {
                return;
            }
            var currencyAmount = Double.Parse(_sourceCurrencyDisplayValue);
            if(currencyAmount == 0)
            {
                return;
            }
            _convertedCurrencyValue = _currencyExchangeService.ConvertCurrency(SourceCurrency, TargetCurrency, currencyAmount);
            RaisePropertyChanged(nameof(TargetCurrencyDisplayValue));
        }

        private void ClearAll()
        {
            SourceCurrencyDisplayValue = null;
            _convertedCurrencyValue = 0;
            RaisePropertyChanged(nameof(TargetCurrencyDisplayValue));
        }

        private void DeleteLastEntry()
        {
            if (_sourceCurrencyDisplayValue?.Length > 0)
            {
                SourceCurrencyDisplayValue = _sourceCurrencyDisplayValue.Remove(_sourceCurrencyDisplayValue.Length - 1, 1);
                if(_sourceCurrencyDisplayValue.Length ==0)
                {
                    _convertedCurrencyValue = 0;
                    RaisePropertyChanged(nameof(TargetCurrencyDisplayValue));
                }
            }
        }

        private void HandlePointEntry()
        {
            if (String.IsNullOrWhiteSpace(_sourceCurrencyDisplayValue))
            {
                SourceCurrencyDisplayValue = "0.";
            }
            else
            {
                if (!_sourceCurrencyDisplayValue.Contains("."))
                {
                    SourceCurrencyDisplayValue = _sourceCurrencyDisplayValue + ".";
                }
            }
        }

        private void HandleDigitEntry(string digit)
        {
            SourceCurrencyDisplayValue = _sourceCurrencyDisplayValue + digit;
        }


    }
}
