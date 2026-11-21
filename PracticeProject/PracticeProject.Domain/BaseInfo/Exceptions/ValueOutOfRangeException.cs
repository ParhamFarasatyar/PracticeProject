using System;

namespace PracticeProject.Domain.BaseInfo.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException() : base("Value is out of the allowed range.")
        {
        }

        public ValueOutOfRangeException(string message) : base(message)
        {
        }

        public ValueOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
