using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCalculator.Models
{
    public class CalculatorResult
    {
        public int Total { get; set; } = 0;
        public string Formula { get; set; } = "0 = 0";
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
