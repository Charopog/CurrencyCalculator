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
                    return firstNumber + secondNumber;
                case "-":
                    return firstNumber - secondNumber;
                case "x":
                    return firstNumber * secondNumber;
                case "÷":
                    return firstNumber / secondNumber;
                default:
                    throw new ArgumentException("Invalid Operator!");
            }
        }
    }
}
