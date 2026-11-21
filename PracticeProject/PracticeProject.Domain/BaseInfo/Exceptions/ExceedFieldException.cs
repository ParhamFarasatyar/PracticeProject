using System;

namespace PracticeProject.Domain.BaseInfo.Exceptions
{
    public class ExceedFieldException : Exception
    {
        public ExceedFieldException() : base("One or more fields exceed the maximum allowed length.")
        {
        }

        public ExceedFieldException(string message) : base(message)
        {
        }

        public ExceedFieldException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
