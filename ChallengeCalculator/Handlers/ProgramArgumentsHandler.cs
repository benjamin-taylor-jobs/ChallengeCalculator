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
            ProgramArguments programArguments = new ProgramArguments();
            if (args.Length > 0 && Enum.IsDefined(typeof(Calculator.CalculatorTypes), args[0]))
                programArguments.CalculatorType = (Calculator.CalculatorTypes)Enum.Parse(typeof(Calculator.CalculatorTypes), args[0], true);
            if (args.Length > 1)
                programArguments.AlternateDelimiter = args[1];
            if (args.Length > 2 && bool.TryParse(args[2], out bool allowNegativeNumbers))
                programArguments.AllowNegativeNumbers = allowNegativeNumbers;
            if (args.Length > 3 && int.TryParse(args[3], out int upperBound))
                programArguments.UpperBound = upperBound;

            return programArguments;
        }
    }
}
