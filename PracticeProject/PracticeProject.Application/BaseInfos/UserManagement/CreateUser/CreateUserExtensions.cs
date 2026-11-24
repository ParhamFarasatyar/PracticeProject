using PracticeProject.Domain.BaseInfo.Exceptions;
using System.Data;

namespace PracticeProject.Application.BaseInfos.UserManagement.CreateUser
{
    public static class CreateUserExtensions
    {
        public static void Validate(this ICreateUser.Request request)
        {
            if (request == null)
                throw new NoNullAllowedException("Request cannot be null");

            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
                throw new AppException("Firstname, email, and password cannot be null");

            if (request.Age < 18 || request.Age > 80)
                throw new AppException("Age value should between 18 and 80.");

            if (request.FirstName.Length > 100 ||
                (request.LastName != null && request.LastName.Length > 100) ||
                request.Email.Length > 250 ||
                request.Password.Length > 150)
                throw new AppException("One or more fields exceed the maximum allowed length.");
        }
    }
}
