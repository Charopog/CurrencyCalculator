using CurrencyCalculator.Xam.Services;
using CurrencyCalculator.Xam.Services.Abstractions;
using System;
using Xunit;

namespace CurrencyCalculator.Tests
{
    public class CalculatorServiceTests
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorServiceTests()
        {
            _calculatorService = new CalculatorService();
        }

        [Fact]
        public void Return30Given10Plus20()
        {
            var result = _calculatorService.Calculate(10, 20, "+");
            Assert.True(result == 30);
        }

        [Fact]
        public void Return10Given30Minus20()
        {
            var result = _calculatorService.Calculate(30, 20, "-");
            Assert.True(result == 10);
        }

        [Fact]
        public void Return25Given5MupltipliedBy5()
        {
            var result = _calculatorService.Calculate(5, 5, "x");
            Assert.True(result == 25);
        }

        [Fact]
        public void CalculateWithWrongOperatorThrowsArgumentException()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                _calculatorService.Calculate(5, 5, "/");
            });
            
            Assert.Equal("Invalid Operator",ex.Message);
        }
    }
}
