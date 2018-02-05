using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyCalculator.Xam.Constants
{
    public enum CalculatorState
    {
        Cleared = 0,
        FirstNumberEntered = 1,
        OperatorEntered = 2,
        ReadyForCalculation
    }
}
