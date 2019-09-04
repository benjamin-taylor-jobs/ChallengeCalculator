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
        readonly List<string> AcceptableDelimiters = new List<string>(){",", @"\n"};

        public CalculatorInputHandler() { }

        /// <summary>
        /// Interprets a formatted delimited string with config data into an object for use by the calculator
        /// </summary>
        /// <param name="userInput">a formatted delimited string</param>
        /// <returns>An object representing the data in the userInput</returns>
        public CalculatorInput InterpretCalculatorInput(string userInput)
        {
            CalculatorInput calculatorInput = new CalculatorInput() {};

            //Split the string by all of the acceptable delimiters
            List<string> splitUserInput = userInput.Split(AcceptableDelimiters.ToArray(), StringSplitOptions.None).ToList();

            //Replace invalid entries with 0
            calculatorInput.Numbers = splitUserInput.Select(x => int.TryParse(x, out int num) ? num : 0).ToList();

            return calculatorInput;
        }
    }
}
