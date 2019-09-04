using ChallengeCalculator.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChallengeCalculator.Handlers
{
    public interface ICalculatorInputHandler
    {
        int UpperBound { get; set; }
        CalculatorInput InterpretCalculatorInput(string userInput);
        void ReplaceAlternativeDelimiterWithArgumentDelimiter(string delimiter);
    }

    public class CalculatorInputHandler : ICalculatorInputHandler
    {
        public List<string> AcceptableDelimiters { get; } = new List<string>() { ",", @"\n" };

        public int UpperBound { get; set; } = 1000;

        public CalculatorInputHandler() { }

        /// <summary>
        /// Interprets a formatted delimited string with config data into an object for use by the calculator
        /// </summary>
        /// <param name="userInput">a formatted delimited string</param>
        /// <returns>An object representing the data in the userInput</returns>
        public CalculatorInput InterpretCalculatorInput(string userInput)
        {
            CalculatorInput calculatorInput = new CalculatorInput() {};

            //Interpret the possible delimiters in the input string
            userInput = InterpretCustomDelimiters(userInput);

            //Split the string by all of the acceptable delimiters
            List<string> splitUserInput = userInput.Split(AcceptableDelimiters.ToArray(), StringSplitOptions.None).ToList();

            //Replace invalid entries with 0
            calculatorInput.Numbers = splitUserInput.Select(x => int.TryParse(x, out int num) ? num : 0).Where(x => x <= UpperBound).ToList();

            return calculatorInput;
        }
        /// <summary>
        /// Replace the Newline (\n) delimiter with an optional argument delimiter
        /// </summary>
        /// <param name="delimiter"></param>
        public void ReplaceAlternativeDelimiterWithArgumentDelimiter(string delimiter)
        {
            if (AcceptableDelimiters.Contains(@"\n"))
                AcceptableDelimiters.Remove(@"\n");
            AddNewDelimiter(delimiter);
        }

        /// <summary>
        /// Extracts delimiters from a user input string and returns the portion that needs delimiter parsing
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns>string with the delimiter formatting removed</returns>
        private string InterpretCustomDelimiters(string userInput)
        {
            //This regular expression will tell us if they are trying to supply a single char delimiter
            Match delimiterFormatMatch = Regex.Match(userInput, @"^\/\/.\\n.*$");
            if (delimiterFormatMatch.Success)
            {
                //Remove the first two forward slashes
                userInput = userInput.Remove(0, 2);

                //Get our single character delimiter and add it to our delimiter list
                AddNewDelimiter(userInput.First().ToString());

                //Remove the single character delimiter
                userInput = userInput.Remove(0, 1);

                //Remove the endline (\n)
                userInput = userInput.Remove(0, 2);
            }

            return userInput;
        }
        private void AddNewDelimiter(string delimiter)
        {
            if (!AcceptableDelimiters.Contains(delimiter))
                AcceptableDelimiters.Add(delimiter);
        }
    }
}
