using ChallengeCalculator.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCalculator.Handlers
{
    public interface ICalculatorInputHandler
    {
        CalculatorInput InterpretCalculatorInput(string userInput);
    }

    public class CalculatorInputHandler : ICalculatorInputHandler
    {
        const int MAX_NUMBERS_ALLOWED = 2;
        readonly List<string> AcceptableDelimiters = new List<string>(){","};

        public CalculatorInputHandler() { }

        /// <summary>
        /// Interprets a formatted delimited string with config data into an object for use by the calculator
        /// </summary>
        /// <param name="userInput">a formatted delimited string</param>
        /// <returns>An object representing the data in the userInput</returns>
        public CalculatorInput InterpretCalculatorInput(string userInput)
        {
            CalculatorInput calculatorInput = new CalculatorInput() { MaxNumbersAllowed = MAX_NUMBERS_ALLOWED };

            List<string> splitUserInput = userInput.Split(AcceptableDelimiters.ToArray(), StringSplitOptions.None).ToList();
            if (splitUserInput.Count > calculatorInput.MaxNumbersAllowed)
                throw new MaxNumbersExceededException($"The Calculator Only Allows up to {calculatorInput.MaxNumbersAllowed} Numbers at a Time");

            //Replace invalid entries with 0
            calculatorInput.Numbers = splitUserInput.Select(x => int.TryParse(x, out int num) ? num : 0).ToList();

            return calculatorInput;
        }
    }
}
