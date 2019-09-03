using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeCalculator.Models;

namespace ChallengeCalculator.Handlers
{
    /// <summary>
    /// An abstract class using polymorphism to call subclass methods for calculations specific to a calculator type
    /// </summary>
    public abstract class Calculator
    {
        public enum CalculatorTypes {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        /// <summary>
        /// Factory Method to get a calculator of the type requested
        /// </summary>
        /// <param name="calculatorType"></param>
        /// <returns></returns>
        public static Calculator CreateCalculator(CalculatorTypes calculatorType)
        {
            switch (calculatorType)
            {
                case CalculatorTypes.Subtraction:
                    return new SubtractionCalculator();
                case CalculatorTypes.Multiplication:
                    return new MultiplicationCalculator();
                case CalculatorTypes.Division:
                    return new DivisionCalculator();
                default:
                    return new AdditionCalculator();
            }
        }

        /// <summary>
        /// Calculate the total of a list of numbers using polymorphism
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="calculator"></param>
        /// <returns></returns>
        public CalculatorResult Calculate(List<int> numbers, Calculator calculator)
        {
            //Handle the first number
            CalculatorResult calculatorResult = new CalculatorResult() {};

            if (numbers.Count > 0 && calculator.IsValidNumbersSet(numbers))
            {
                string FORMULA_DELIMITER = calculator.GetFormulaDelimiter();
                StringBuilder formula = new StringBuilder();

                //Handle the first number without any airithmetic operation
                calculatorResult.Total = numbers[0];
                formula.Append((numbers[0]).ToString());

                //Handle the subsequent numbers, operating between each one and the previous result
                foreach (int number in numbers.GetRange(1, numbers.Count - 1))
                {
                    calculatorResult.Total = calculator.ExecuteAirithmeticOperation(calculatorResult.Total, number);
                    formula.Append($"{FORMULA_DELIMITER}{number}");
                }

                //Attach the total to the formula concatenation
                calculatorResult.Formula = $"{formula.ToString()} = {calculatorResult.Total}";
            }

            return calculatorResult;
        }

        public abstract string GetFormulaDelimiter();
        public abstract int ExecuteAirithmeticOperation(int currentTotal, int nextValue);
        public abstract bool IsValidNumbersSet(List<int> numbers);
    }

    class AdditionCalculator : Calculator
    {
        public override string GetFormulaDelimiter() { return "+"; }
        public override int ExecuteAirithmeticOperation(int currentTotal, int nextValue) { return currentTotal + nextValue; }
        public override bool IsValidNumbersSet(List<int> numbers) { return true; }
    }
    class SubtractionCalculator : Calculator
    {
        public override string GetFormulaDelimiter() { return "-"; }
        public override int ExecuteAirithmeticOperation(int currentTotal, int nextValue) { return currentTotal - nextValue; }
        public override bool IsValidNumbersSet(List<int> numbers) { return true; }
    }
    class MultiplicationCalculator : Calculator
    {
        public override string GetFormulaDelimiter() { return "*"; }
        public override int ExecuteAirithmeticOperation(int currentTotal, int nextValue) { return currentTotal * nextValue; }
        public override bool IsValidNumbersSet(List<int> numbers) { return true; }
    }
    class DivisionCalculator : Calculator
    {
        public override string GetFormulaDelimiter() { return "/"; }
        /// <summary>
        /// Divide two numbers in a set of numbers (Uses Integer Division since it was not specified in the exercise)
        /// </summary>
        /// <param name="currentTotal"></param>
        /// <param name="nextValue"></param>
        /// <returns>Result of two numbers divided by integer division</returns>
        public override int ExecuteAirithmeticOperation(int currentTotal, int nextValue) { return currentTotal / nextValue; }
        public override bool IsValidNumbersSet(List<int> numbers)
        {
            if (numbers.Count > 1 && numbers.Contains(0))
                throw new DivideByZeroException("Cannot Divide by Zero");
            return true;
        }
    }
}
