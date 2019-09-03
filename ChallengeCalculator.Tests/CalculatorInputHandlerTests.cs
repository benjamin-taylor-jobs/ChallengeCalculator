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

            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_OneValidNumberWithNoDelimiter_SuccessfullyParsed()
        {
            string userInput = "5000";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 5000);
        }

        [TestMethod]
        public void InterpretCalculatorInput_OneInvalidNumberWithNoDelimiter_SuccessfullyParsed()
        {
            string userInput = "5t232ac;";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_TwoValidNumbersWithDelimiter_SuccessfullyParsed()
        {
            string userInput = "1,20";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 1);
            Assert.IsTrue(calculatorInput.Numbers[1] == 20);
        }

        [TestMethod]
        public void InterpretCalculatorInput_TwoInvalidNumbersWithDelimiter_SuccessfullyParsed()
        {
            string userInput = "1'2'f234tfava ,20 2f2323a2f";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_MultipleInvalidNumbers_ReturnSuccessful()
        {
            string userInput = "1'2',f234tfava ,20 2f2323a2f";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 3);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }

        [TestMethod]
        public void InterpretCalculatorInput_MultipleValidNumbers_ReturnSuccessful()
        {
            string userInput = "243,-2,5,-4,9";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 5);
            Assert.IsTrue(calculatorInput.Numbers[0] == 243);
            Assert.IsTrue(calculatorInput.Numbers[1] == -2);
            Assert.IsTrue(calculatorInput.Numbers[2] == 5);
            Assert.IsTrue(calculatorInput.Numbers[3] == -4);
            Assert.IsTrue(calculatorInput.Numbers[4] == 9);
        }
    }
}
