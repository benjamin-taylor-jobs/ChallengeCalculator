using ChallengeCalculator.Handlers;
using ChallengeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo consoleKeyInfo;
            Console.TreatControlCAsInput = true;

            StringBuilder userInput = new StringBuilder();
            CalculatorHandler calculatorHandler = new CalculatorHandler(new CalculatorInputHandler(), new ProgramArgumentsHandler());

            Console.WriteLine("Please Enter a String for Calculation or Press CTRL + C to Exit");

            do
            {
                consoleKeyInfo = Console.ReadKey();

                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    //Move down to the next line for displaying calculation results
                    Console.WriteLine();

                    //Calculate the results and display them
                    CalculatorHandler.DisplayCalculatorResult(calculatorHandler.Calculate(userInput.ToString(), args));

                    //Reset the StringBuilder for possible next input
                    userInput = new StringBuilder();
                }
                else if (consoleKeyInfo.Key == ConsoleKey.Backspace)
                    userInput.Remove(userInput.Length - 1, 1);
                else
                    userInput.Append(consoleKeyInfo.KeyChar);

            } while (!(consoleKeyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && consoleKeyInfo.Key.HasFlag(ConsoleKey.C)));
        }
    }
}
