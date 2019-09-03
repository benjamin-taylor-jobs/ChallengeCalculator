using ChallengeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCalculator.Handlers
{
    public interface IProgramArgumentsHandler
    {
        ProgramArguments InterpretProgramArguments(string[] args);
    }

    /// <summary>
    /// Inteprets the arguments from the application provided into the console application
    /// </summary>
    class ProgramArgumentsHandler : IProgramArgumentsHandler
    {
        public ProgramArguments InterpretProgramArguments(string[] args)
        {
            return new ProgramArguments
            {
                CalculatorType = args.Length > 0 && Enum.IsDefined(typeof(Calculator.CalculatorTypes), args[0]) ? (Calculator.CalculatorTypes)Enum.Parse(typeof(Calculator.CalculatorTypes), args[0], true) : Calculator.CalculatorTypes.Addition,
                AlternateDelimiter = args.Length > 1 ? args[1] : "",
                AllowNegativeNumbers = args.Length > 2 && bool.TryParse(args[2], out bool allowNegativeNumbers) ? allowNegativeNumbers : true,
                UpperBound = args.Length > 3 && int.TryParse(args[3], out int upperBound) ? upperBound : 0
            };
        }
    }
}
