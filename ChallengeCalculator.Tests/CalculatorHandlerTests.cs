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
    public class CalculatorHandlerTests
    {
        ICalculatorInputHandler calculatorInputHandler = NSubstitute.Substitute.For<ICalculatorInputHandler>();
        IProgramArgumentsHandler programArgumentsHandler = NSubstitute.Substitute.For<IProgramArgumentsHandler>();

        public void Calculate_NoNumbers_SuccessfulResult()
        {
            string[] args = null;
            string userInput = "";
            calculatorInputHandler.InterpretCalculatorInput(Arg.Any<string>()).Returns(
                new CalculatorInput
                {
                    MaxNumbersAllowed = 2,
                    Numbers = new List<int>() { }
                }
            );
            programArgumentsHandler.InterpretProgramArguments(Arg.Any<string[]>()).Returns(new ProgramArguments());

            CalculatorHandler calculatorHandler = new CalculatorHandler(calculatorInputHandler, programArgumentsHandler);
            CalculatorResult calculatorResult = calculatorHandler.Calculate(userInput, args);

            Assert.IsTrue(calculatorResult.Total == 0);
            Assert.IsTrue(calculatorResult.Formula.Equals("0 = 0"));
            Assert.IsTrue(calculatorResult.ErrorMessage.Length == 0);
        }

        [TestMethod]
        public void Calculate_OneNumberZero_SuccessfulResult()
        {
            string[] args = null;
            string userInput = "";
            calculatorInputHandler.InterpretCalculatorInput(Arg.Any<string>()).Returns(
                new CalculatorInput
                {
                    MaxNumbersAllowed = 2,
                    Numbers = new List<int>() { 0 }
                }
            );
            programArgumentsHandler.InterpretProgramArguments(Arg.Any<string[]>()).Returns(new ProgramArguments());

            CalculatorHandler calculatorHandler = new CalculatorHandler(calculatorInputHandler, programArgumentsHandler);
            CalculatorResult calculatorResult = calculatorHandler.Calculate(userInput, args);

            Assert.IsTrue(calculatorResult.Total == 0);
            Assert.IsTrue(calculatorResult.Formula.Equals("0 = 0"));
            Assert.IsTrue(calculatorResult.ErrorMessage.Length == 0);
        }

        [TestMethod]
        public void Calculate_OneNumberNonZero_SuccessfulResult()
        {
            string[] args = null;
            string userInput = "";
            calculatorInputHandler.InterpretCalculatorInput(Arg.Any<string>()).Returns(
                new CalculatorInput
                {
                    MaxNumbersAllowed = 2,
                    Numbers = new List<int>() { 5000 }
                }
            );
            programArgumentsHandler.InterpretProgramArguments(Arg.Any<string[]>()).Returns(new ProgramArguments());

            CalculatorHandler calculatorHandler = new CalculatorHandler(calculatorInputHandler, programArgumentsHandler);
            CalculatorResult calculatorResult = calculatorHandler.Calculate(userInput, args);

            Assert.IsTrue(calculatorResult.Total == 5000);
            Assert.IsTrue(calculatorResult.Formula.Equals("5000 = 5000"));
            Assert.IsTrue(calculatorResult.ErrorMessage.Length == 0);
        }

        [TestMethod]
        public void Calculate_MoreThanOneNumberZeroDivision_CannotDivideByZero()
        {
            string[] args = null;
            string userInput = "";
            calculatorInputHandler.InterpretCalculatorInput(Arg.Any<string>()).Returns(
                new CalculatorInput
                {
                    MaxNumbersAllowed = 2,
                    Numbers = new List<int>() { 24,0 }
                }
            );
            programArgumentsHandler.InterpretProgramArguments(Arg.Any<string[]>())
                .Returns(new ProgramArguments {
                    CalculatorType = Calculator.CalculatorTypes.Division
                });

            CalculatorHandler calculatorHandler = new CalculatorHandler(calculatorInputHandler, programArgumentsHandler);
            CalculatorResult calculatorResult = calculatorHandler.Calculate(userInput, args);

            Assert.IsTrue(calculatorResult.Total == 0);
            Assert.IsTrue(calculatorResult.Formula.Equals("0 = 0"));
            Assert.IsTrue(calculatorResult.ErrorMessage.ToUpper().Contains("CANNOT DIVIDE BY ZERO"));
        }

        [TestMethod]
        public void Calculate_MaxNumbersAllowedExceeded_MaxNumbersAllowedExceeded()
        {
            string[] args = null;
            string userInput = "";
            const int MAX_NUMBERS_ALLOWED = 2;
            calculatorInputHandler.InterpretCalculatorInput(Arg.Any<string>()).Returns(x =>
            {
                throw new MaxNumbersExceededException($"The Calculator Only Allows up to {MAX_NUMBERS_ALLOWED} Numbers at a Time");
            });
            programArgumentsHandler.InterpretProgramArguments(Arg.Any<string[]>()).Returns(new ProgramArguments());

            CalculatorHandler calculatorHandler = new CalculatorHandler(calculatorInputHandler, programArgumentsHandler);
            CalculatorResult calculatorResult = calculatorHandler.Calculate(userInput, args);

            Assert.IsTrue(calculatorResult.Total == 0);
            Assert.IsTrue(calculatorResult.Formula.Equals("0 = 0"));
            Assert.IsTrue(calculatorResult.ErrorMessage.ToUpper().Contains("THE CALCULATOR ONLY ALLOWS UP TO 2 NUMBERS AT A TIME"));
        }

        [TestMethod]
        public void Calculate_CalculatorInputException_GenericException()
        {
            string[] args = null;
            string userInput = "";
            calculatorInputHandler.InterpretCalculatorInput(Arg.Any<string>()).Returns(x =>
            {
                throw new Exception($"This is a Test");
            });
            programArgumentsHandler.InterpretProgramArguments(Arg.Any<string[]>()).Returns(new ProgramArguments());

            CalculatorHandler calculatorHandler = new CalculatorHandler(calculatorInputHandler, programArgumentsHandler);
            CalculatorResult calculatorResult = calculatorHandler.Calculate(userInput, args);

            Assert.IsTrue(calculatorResult.Total == 0);
            Assert.IsTrue(calculatorResult.Formula.Equals("0 = 0"));
            Assert.IsTrue(calculatorResult.ErrorMessage.ToUpper().Contains("ENCOUNTERED AN ERROR ATTEMPTING TO INTEPRET THE CALCULATOR INPUT"));
        }
    }
}
