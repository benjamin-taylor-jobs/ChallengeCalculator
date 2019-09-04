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
            string userInput = @"";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_OneValidNumberWithNoDelimiter_SuccessfullyParsed()
        {
            string userInput = @"5000";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 5000);
        }

        [TestMethod]
        public void InterpretCalculatorInput_OneInvalidNumberWithNoDelimiter_SuccessfullyParsed()
        {
            string userInput = @"5t232ac;";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_TwoValidNumbersWithCommaDelimiter_SuccessfullyParsed()
        {
            string userInput = @"1,20";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 1);
            Assert.IsTrue(calculatorInput.Numbers[1] == 20);
        }

        [TestMethod]
        public void InterpretCalculatorInput_TwoValidNumbersWithNewlineDelimiter_SuccessfullyParsed()
        {
            string userInput = @"1\n20";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 1);
            Assert.IsTrue(calculatorInput.Numbers[1] == 20);
        }

        [TestMethod]
        public void InterpretCalculatorInput_TwoInvalidNumbersWithCommaDelimiter_SuccessfullyParsed()
        {
            string userInput = @"1'2'f234tfava ,20 2f2323a2f";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_TwoInvalidNumbersWithNewlineDelimiter_SuccessfullyParsed()
        {
            string userInput = @"1'2'f234tfava \n20 2f2323a2f";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_MultipleInvalidNumbersWithCommaDelimiter_ReturnSuccessful()
        {
            string userInput = @"1'2',f234tfava ,20 2f2323a2f";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 3);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
            Assert.IsTrue(calculatorInput.Numbers[2] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_MultipleInvalidNumbersWithNewlineDelimiter_ReturnSuccessful()
        {
            string userInput = @"1'2'\nf234tfava \n20 2f2323a2f";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 3);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
            Assert.IsTrue(calculatorInput.Numbers[2] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_MultipleValidNumbersWithCommanAndNewlineDelimiters_ReturnSuccessful()
        {
            string userInput = @"1\n2,3";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 3);
            Assert.IsTrue(calculatorInput.Numbers[0] == 1);
            Assert.IsTrue(calculatorInput.Numbers[1] == 2);
            Assert.IsTrue(calculatorInput.Numbers[2] == 3);
        }
    }
}
