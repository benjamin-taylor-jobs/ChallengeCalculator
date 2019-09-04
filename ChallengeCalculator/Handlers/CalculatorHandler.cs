using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeCalculator.Common.Exceptions;
using ChallengeCalculator.Models;

namespace ChallengeCalculator.Handlers
{
    public class CalculatorHandler
    {
        Calculator calculator;
        ICalculatorInputHandler calculatorInputHandler;
        IProgramArgumentsHandler programArgumentsHandler;

        public CalculatorHandler(ICalculatorInputHandler calculatorInputHandler, IProgramArgumentsHandler programArgumentsHandler)
        {
            this.calculatorInputHandler = calculatorInputHandler;
            this.programArgumentsHandler = programArgumentsHandler;
            this.calculator = Calculator.CreateCalculator(Calculator.CalculatorTypes.Addition);
        }

        /// <summary>
        /// Calculate the aggregation of numbers in a delimited string based on console arguments and input
        /// </summary>
        /// <param name="userInput">numbers and delimiters in a delimited format</param>
        /// <param name="args">applpication arguments</param>
        /// <returns>A result of the calculation based on the configuration</returns>
        public CalculatorResult Calculate(string userInput, string[] args)
        {
            CalculatorResult calculatorResult = new CalculatorResult();

            try
            {
                ProgramArguments programArguments = programArgumentsHandler.InterpretProgramArguments(args);
                if (programArguments.AlternateDelimiter.Length > 0)
                    calculatorInputHandler.ReplaceAlternativeDelimiterWithArgumentDelimiter(programArguments.AlternateDelimiter);
                CalculatorInput calculatorInput = calculatorInputHandler.InterpretCalculatorInput(userInput);
                if (!programArguments.AllowNegativeNumbers && calculatorInput.Numbers.Any(x => x < 0))
                    throw new NegativeNumberException("Negative numbers are not allowed: " + string.Join(",", calculatorInput.Numbers.Where(x => x < 0)));

                try
                {
                    calculatorResult = calculator.Calculate(calculatorInput.Numbers, Calculator.CreateCalculator(programArguments.CalculatorType));
                }
                catch (DivideByZeroException ex)
                {
                    calculatorResult.ErrorMessage = $"{ex.Message}";
                }
                catch (Exception ex)
                {
                    calculatorResult.ErrorMessage = $"Encountered an Error Attempting to Run Calculation: {ex.ToString()}";
                }
            }
            catch (MaxNumbersExceededException ex)
            {
                calculatorResult.ErrorMessage = $"{ex.Message}";
            }
            catch (NegativeNumberException ex)
            {
                calculatorResult.ErrorMessage = $"{ex.Message}";
            }
            catch (Exception ex)
            {
                calculatorResult.ErrorMessage = $"Encountered an Error Attempting to Intepret the Calculator Input: {ex.ToString()}";
            }

            return calculatorResult;
        }

        /// <summary>
        /// Write out to console a calculator result
        /// </summary>
        /// <param name="calculatorResult"></param>
        public static void DisplayCalculatorResult(CalculatorResult calculatorResult)
        {
            if (calculatorResult.ErrorMessage.Length > 0)
                Console.WriteLine(calculatorResult.ErrorMessage);
            else
            {
                Console.WriteLine($"Total: {calculatorResult.Total}");
                Console.WriteLine($"Formula: {calculatorResult.Formula}");
            }
        }
    }
}
