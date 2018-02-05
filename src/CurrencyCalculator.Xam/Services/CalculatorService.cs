using CurrencyCalculator.Xam.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyCalculator.Xam.Services
{
    public class CalculatorService : ICalculatorService
    {
        public double Calculate(double firstNumber, double secondNumber, string mathOperator)
        {
            switch (mathOperator)
            {
                case "+":
                    return Math.Round(firstNumber + secondNumber, 2);
                case "-":
                    return Math.Round(firstNumber - secondNumber, 2);
                case "x":
                    return Math.Round(firstNumber * secondNumber, 2);
                case "÷":
                    return Math.Round(firstNumber / secondNumber, 2);
                default:
                    throw new ArgumentException("Invalid Operator");
            }
        }
    }
}
