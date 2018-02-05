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
	public class CalculatorPageViewModel : ViewModelBase
    {
        private readonly ICalculatorService _calculatorService;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        private double? _firstOperand;
        private string _mathOperator;

        public CalculatorPageViewModel(INavigationService navigationService, ICalculatorService calculatorService,
            ICurrencyExchangeService currencyExchangeService) : base(navigationService)
        {
            _currencyExchangeService = currencyExchangeService
                ?? throw new ArgumentNullException(nameof(currencyExchangeService));

            _calculatorService = calculatorService 
                ?? throw new ArgumentNullException(nameof(calculatorService));

            DigitEntryCommand = new DelegateCommand<string>(HandleDigitEntry);
            OperatorEntryCommand = new DelegateCommand<string>(HandleOperatorEntry);
            PointEntryCommand = new DelegateCommand(HandlePointEntry);
            DeleteCommand = new DelegateCommand(DeleteLastEntry);
            ClearCommand = new DelegateCommand(ClearAll);
            EqualsCommand = new DelegateCommand(ExecuteEquals);
            NavigateToCurrencyConverterCommand = new DelegateCommand(NavigateToCurrencyConverter);
            PlusMinusEntryCommand = new DelegateCommand(HandlePlusMinusEntry);
        }

        public DelegateCommand<string> DigitEntryCommand { get; private set; }
        public DelegateCommand<string> OperatorEntryCommand { get; private set; }
        public DelegateCommand PlusMinusEntryCommand { get; private set; }
        public DelegateCommand PointEntryCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }
        public DelegateCommand EqualsCommand { get; private set; }
        public DelegateCommand NavigateToCurrencyConverterCommand { get; private set; }

        public string CalculationsDisplayValue
        {
            get
            {
                return $"{_firstOperand} {_mathOperator}";
            }
        }

        private string _resultDisplayValue;
        public string ResultDisplayValue
        {
            get { return _resultDisplayValue; }
            set { SetProperty(ref _resultDisplayValue, value); }
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await _currencyExchangeService.GetLatestExchangeRates();
        }

        private void HandlePlusMinusEntry()
        {
            if (String.IsNullOrWhiteSpace(_resultDisplayValue))
            {
                return;
            }
            if (_resultDisplayValue.Contains("-"))
            {
                ResultDisplayValue = _resultDisplayValue.Remove(0, 1);
            }
            else
            {
                ResultDisplayValue = "-" + _resultDisplayValue;
            }
        }

        private async void NavigateToCurrencyConverter()
        {
            await NavigationService.NavigateAsync("CurrencyConvertPage", null, true, false);
        }

        private void ExecuteEquals()
        {
            if (String.IsNullOrWhiteSpace(_resultDisplayValue) && _firstOperand == null && String.IsNullOrWhiteSpace(_mathOperator))
            {
                return;
            }
            double secondNumber = Double.Parse(_resultDisplayValue);

            double result = _calculatorService.Calculate(_firstOperand.Value, secondNumber, _mathOperator);
            ResultDisplayValue = result.ToString();
            _firstOperand = null;
            _mathOperator = null;
            RaisePropertyChanged(nameof(CalculationsDisplayValue));
        }

        private void ClearAll()
        {
            ResultDisplayValue = null;
            _firstOperand = null;
            _mathOperator = null;
            RaisePropertyChanged(nameof(CalculationsDisplayValue));

        }

        private void DeleteLastEntry()
        {
            if(_resultDisplayValue?.Length>0)
            {
                ResultDisplayValue = _resultDisplayValue.Remove(_resultDisplayValue.Length-1, 1);
            }
        }

        private void HandlePointEntry()
        {
            if(String.IsNullOrWhiteSpace(_resultDisplayValue))
            {
                ResultDisplayValue = "0.";
            }
            else
            {
                if(!_resultDisplayValue.Contains("."))
                {
                    ResultDisplayValue = _resultDisplayValue + ".";
                }
            }
        }

        private void HandleOperatorEntry(string mathOperator)
        {
            if (String.IsNullOrWhiteSpace(_resultDisplayValue))
            {
                return;
            }
            if (_firstOperand == null)
            {
                _firstOperand = Double.Parse(_resultDisplayValue);
                _mathOperator = mathOperator;
                RaisePropertyChanged(nameof(CalculationsDisplayValue));
                ResultDisplayValue = null;
                return;
            }
            if (_firstOperand != null && !String.IsNullOrWhiteSpace(_mathOperator))
            {
                double secondNumber = Double.Parse(_resultDisplayValue);

                double result = _calculatorService.Calculate(_firstOperand.Value, secondNumber, _mathOperator);
                _firstOperand = result;
                _mathOperator = mathOperator;
                RaisePropertyChanged(nameof(CalculationsDisplayValue));
                ResultDisplayValue = null;
            }
        }

        private void HandleDigitEntry(string digit)
        {
            ResultDisplayValue = _resultDisplayValue + digit;
        }
    }
}
