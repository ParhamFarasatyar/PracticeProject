using System;

namespace PracticeProject.Domain.BaseInfo.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException() : base("This email address is already registered.")
        {
        }

        public DuplicateEmailException(string message) : base(message)
        {
        }

        public DuplicateEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
