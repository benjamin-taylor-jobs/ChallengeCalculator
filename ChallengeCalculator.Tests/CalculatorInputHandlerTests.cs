using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeCalculator.Handlers;
using ChallengeCalculator.Common.Exceptions;

namespace ChallengeCalculator.Tests
{
    [TestClass]
    public class CalculatorInputHandlerTests
    {
        [TestMethod]
        public void InterpretCalculatorInput_NoNumbers_SuccessfullyParsed()
        {
            string userInput = "";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.MaxNumbersAllowed == 2);
            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_OneValidNumberWithNoDelimiter_SuccessfullyParsed()
        {
            string userInput = "5000";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.MaxNumbersAllowed == 2);
            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 5000);
        }

        [TestMethod]
        public void InterpretCalculatorInput_OneInvalidNumberWithNoDelimiter_SuccessfullyParsed()
        {
            string userInput = "5t232ac;";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.MaxNumbersAllowed == 2);
            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_TwoValidNumbersWithDelimiter_SuccessfullyParsed()
        {
            string userInput = "1,20";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.MaxNumbersAllowed == 2);
            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 1);
            Assert.IsTrue(calculatorInput.Numbers[1] == 20);
        }

        [TestMethod]
        public void InterpretCalculatorInput_TwoInvalidNumbersWithDelimiter_SuccessfullyParsed()
        {
            string userInput = "1'2'f234tfava ,20 2f2323a2f";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.MaxNumbersAllowed == 2);
            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(MaxNumbersExceededException), "The Calculator Only Allows up to X Numbers at a Time")]
        public void InterpretCalculatorInput_ExceedMaximumNumbers_ErrorMessage()
        {
            string userInput = "1'2',f234tfava ,20 2f2323a2f";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.MaxNumbersAllowed == 2);
            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }
    }
}
