namespace CurrencyCalculator.Xam.Services.Abstractions
{
    public interface ICalculatorService
    {
        double Calculate(double firstNumber, double secondNumber, string mathOperator);
    }
}