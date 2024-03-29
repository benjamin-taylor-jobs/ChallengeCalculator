﻿using ChallengeCalculator.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCalculator.Models
{
    public class ProgramArguments
    {
        public Calculator.CalculatorTypes CalculatorType { get; set; } = Calculator.CalculatorTypes.Addition;
        public string AlternateDelimiter { get; set; } = "";
        public bool AllowNegativeNumbers { get; set; } = false;
        public bool UpperBoundExists { get; set; } = false;
        public int UpperBound { get; set; } = 0;
    }
}
