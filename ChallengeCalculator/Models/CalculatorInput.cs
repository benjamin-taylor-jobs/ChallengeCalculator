using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCalculator
{
    public class CalculatorInput
    {
        public int MaxNumbersAllowed { get; set; } = 2;
        public List<int> Numbers { get; set; } = new List<int>();
    }
}
