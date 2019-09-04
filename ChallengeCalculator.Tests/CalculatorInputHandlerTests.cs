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
            string userInput = @"500";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 1);
            Assert.IsTrue(calculatorInput.Numbers[0] == 500);
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
        public void InterpretCalculatorInput_MultipleValidNumbersWithCommaAndNewlineDelimiters_ReturnSuccessful()
        {
            string userInput = @"1\n2,3";

            CalculatorInput calculatorInput = new CalculatorInputHandler().InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 3);
            Assert.IsTrue(calculatorInput.Numbers[0] == 1);
            Assert.IsTrue(calculatorInput.Numbers[1] == 2);
            Assert.IsTrue(calculatorInput.Numbers[2] == 3);
        }

        [TestMethod]
        public void InterpretCalculatorInput_MultipleValidNumbersWithCommaAndArgumentDelimiters_ReturnSuccessful()
        {
            string userInput = @"1|2,3";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            calculatorInputHandler.ReplaceAlternativeDelimiterWithArgumentDelimiter("|");
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 3);
            Assert.IsTrue(calculatorInput.Numbers[0] == 1);
            Assert.IsTrue(calculatorInput.Numbers[1] == 2);
            Assert.IsTrue(calculatorInput.Numbers[2] == 3);
        }

        [TestMethod]
        public void InterpretCalculatorInput_NumbersOver1000Excluded_ReturnSuccessful()
        {
            string userInput = @"2,1001,6";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            calculatorInputHandler.ReplaceAlternativeDelimiterWithArgumentDelimiter("|");
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 2);
            Assert.IsTrue(calculatorInput.Numbers[1] == 6);
        }

        [TestMethod]
        public void InterpretCalculatorInput_ArgumentProvidedUpperBoundExcluded_ReturnSuccessful()
        {
            const int UPPER_BOUND = 2000;
            string userInput = @"2,1001,2001,6";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            calculatorInputHandler.UpperBound = UPPER_BOUND;
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInput.Numbers.Count == 3);
            Assert.IsTrue(calculatorInput.Numbers[0] == 2);
            Assert.IsTrue(calculatorInput.Numbers[1] == 1001);
            Assert.IsTrue(calculatorInput.Numbers[2] == 6);
        }

        [TestMethod]
        public void InterpretCustomDelimiters_SingleCharacterDelimiterNotProvided_ReturnSuccessful()
        {
            string userInput = @"//\n2;5";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInputHandler.AcceptableDelimiters.Count == 2);
            Assert.IsTrue(!calculatorInputHandler.AcceptableDelimiters.Contains(";"));
            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }

        [TestMethod]
        public void InterpretCustomDelimiters_SingleCharacterDelimiterProvided_ReturnSuccessful()
        {
            string userInput = @"//;\n2;5";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInputHandler.AcceptableDelimiters.Count == 3);
            Assert.IsTrue(calculatorInputHandler.AcceptableDelimiters.Contains(";"));
            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 2);
            Assert.IsTrue(calculatorInput.Numbers[1] == 5);
        }

        [TestMethod]
        public void InterpretCustomDelimiters_MoreThanSingleCharacterDelimiterProvided_ReturnSuccessful()
        {
            string userInput = @"//;;\n2;5";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInputHandler.AcceptableDelimiters.Count == 2);
            Assert.IsTrue(!calculatorInputHandler.AcceptableDelimiters.Contains(";"));
            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }

        [TestMethod]
        public void InterpretCustomDelimiters_SingleMultiCharacterDelimiterProvided_ReturnSuccessful()
        {
            string userInput = @"//[***]\n11***22***33";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInputHandler.AcceptableDelimiters.Count == 3);
            Assert.IsTrue(calculatorInputHandler.AcceptableDelimiters.Contains("***"));
            Assert.IsTrue(calculatorInput.Numbers.Count == 3);
            Assert.IsTrue(calculatorInput.Numbers[0] == 11);
            Assert.IsTrue(calculatorInput.Numbers[1] == 22);
            Assert.IsTrue(calculatorInput.Numbers[2] == 33);
        }

        [TestMethod]
        public void InterpretCustomDelimiters_MultipleMultiCharacterDelimitersProvided_TreatedLikeNoDelimitersProvided()
        {
            string userInput = @"//[***][26]\n11***22***33";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInputHandler.AcceptableDelimiters.Count == 2);
            Assert.IsTrue(!calculatorInputHandler.AcceptableDelimiters.Contains("***"));
            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }

        [TestMethod]
        public void InterpretCustomDelimiters_ComboSingleMultiCharacterDelimitersProvided_TreatedLikeNoDelimitersProvided()
        {
            string userInput = @"//;[***][26]\n11***22***33";

            CalculatorInputHandler calculatorInputHandler = new CalculatorInputHandler();
            CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);

            Assert.IsTrue(calculatorInputHandler.AcceptableDelimiters.Count == 2);
            Assert.IsTrue(!calculatorInputHandler.AcceptableDelimiters.Contains("***"));
            Assert.IsTrue(calculatorInput.Numbers.Count == 2);
            Assert.IsTrue(calculatorInput.Numbers[0] == 0);
            Assert.IsTrue(calculatorInput.Numbers[1] == 0);
        }
    }
}
