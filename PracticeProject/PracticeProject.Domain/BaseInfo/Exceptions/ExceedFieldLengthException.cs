using System;

namespace PracticeProject.Domain.BaseInfo.Exceptions
{
    public class ExceedFieldLengthException : Exception
    {
        public ExceedFieldLengthException() : base("One or more fields exceed the maximum allowed length.")
        {
        }

        public ExceedFieldLengthException(string message) : base(message)
        {
        }

        public ExceedFieldLengthException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
