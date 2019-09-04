using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCalculator.Common.Exceptions
{
    public class MaxNumbersExceededException : Exception
    {
        public MaxNumbersExceededException() { }

        public MaxNumbersExceededException(string message) : base(message) { }

        public MaxNumbersExceededException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NegativeNumberException : Exception
    {
        public NegativeNumberException() { }

        public NegativeNumberException(string message) : base(message) { }

        public NegativeNumberException(string message, Exception innerException) : base(message, innerException) { }
    }
}
