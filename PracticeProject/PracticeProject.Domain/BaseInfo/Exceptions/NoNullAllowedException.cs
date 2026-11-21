using System;

namespace PracticeProject.Domain.BaseInfo.Exceptions
{
    public class NoNullAllowedException : Exception
    {
        public NoNullAllowedException() : base("Null values are not allowed.")
        {
        }

        public NoNullAllowedException(string message) : base(message)
        {
        }

        public NoNullAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
