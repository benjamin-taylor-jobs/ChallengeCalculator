using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeCalculator.Handlers;
using ChallengeCalculator.Common.Exceptions;
using NSubstitute;
using System.Collections.Generic;
using ChallengeCalculator.Models;

namespace ChallengeCalculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        Calculator calculator;

        [TestMethod]
        public void Calculate_NoNumbers_DefaultResult()
        {
            List<int> numbers = new List<int>();

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Addition);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 0);
            Assert.IsTrue(calculatorResult.Formula.Equals("0 = 0"));
        }

        [TestMethod]
        public void Calculate_AdditionOneNumber_SuccessfulResult()
        {
            List<int> numbers = new List<int> { 5000 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Addition);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 5000);
            Assert.IsTrue(calculatorResult.Formula.Equals("5000 = 5000"));
        }

        [TestMethod]
        public void Calculate_AdditionMoreThanOneNumber_SuccessfulResult()
        {
            List<int> numbers = new List<int> { 20, 1 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Addition);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 21);
            Assert.IsTrue(calculatorResult.Formula.Equals("20+1 = 21"));
        }

        [TestMethod]
        public void Calculate_SubtractionOneNumber_SuccessfulResult()
        {
            List<int> numbers = new List<int> { 5000 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Subtraction);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 5000);
            Assert.IsTrue(calculatorResult.Formula.Equals("5000 = 5000"));
        }

        [TestMethod]
        public void Calculate_SubtractionMoreThanOneNumber_SuccessfulResult()
        {
            List<int> numbers = new List<int> { 20, 1 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Subtraction);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 19);
            Assert.IsTrue(calculatorResult.Formula.Equals("20-1 = 19"));
        }

        [TestMethod]
        public void Calculate_MultiplicationOneNumber_SuccessfulResult()
        {
            List<int> numbers = new List<int> { 5000 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Multiplication);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 5000);
            Assert.IsTrue(calculatorResult.Formula.Equals("5000 = 5000"));
        }

        [TestMethod]
        public void Calculate_MultiplicationMoreThanOneNumber_SuccessfulResult()
        {
            List<int> numbers = new List<int> { 20, 1 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Multiplication);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 20);
            Assert.IsTrue(calculatorResult.Formula.Equals("20*1 = 20"));
        }

        [TestMethod]
        public void Calculate_DivisionOneNumber_SuccessfulResult()
        {
            List<int> numbers = new List<int> { 5000 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Division);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 5000);
            Assert.IsTrue(calculatorResult.Formula.Equals("5000 = 5000"));
        }

        [TestMethod]
        public void Calculate_DivisionMoreThanOneNumber_SuccessfulResult()
        {
            List<int> numbers = new List<int> { 20, 4 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Division);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);

            Assert.IsTrue(calculatorResult.Total == 5);
            Assert.IsTrue(calculatorResult.Formula.Equals("20/4 = 5"));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Cannot Divide by Zero")]
        public void Calculate_DivisionInvalidNumber_DivideByZerioException()
        {
            List<int> numbers = new List<int> { 20, 0 };

            calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Division);
            CalculatorResult calculatorResult = calculator.Calculate(numbers, calculator);
        }
    }
}
