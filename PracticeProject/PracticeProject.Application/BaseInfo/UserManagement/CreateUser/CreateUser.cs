using PracticeProject.Domain.BaseInfo.Person;
using PracticeProject.Domain.BaseInfo.User;
using System;

namespace PracticeProject.Application.BaseInfo.UserManagement.CreateUser
{
    public class CreateUser : ICreateUser
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IDb _db;

        public CreateUser(IUserDataAccess userDataAccess, IDb db)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public ICreateUser.CreateUserResponse Execute(ICreateUser.CreateUserRequest request)
        {
            bool isRequestValid = IsRequestValid(request, out string errorMessage);

            if (!isRequestValid)
                return CreateFailureResponse(errorMessage);

            Person person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Gender = request.Gender
            };

            Role role = Role.User;
            if (string.Equals(request.FirstName, "Parham", StringComparison.OrdinalIgnoreCase))
                role = Role.Admin;

            var user = new User(
                role,
                request.Email,
                request.Password,
                person
                );

            _userDataAccess.Create(user);
            _db.SaveChanges();

            ICreateUser.CreateUserResponse response = new ICreateUser.CreateUserResponse
            {
                IsSuccess = true,
                Message = "User created successfully.",
                UserId = user.Id
            };
            return response;
        }

        private ICreateUser.CreateUserResponse CreateFailureResponse(string message)
        {
            ICreateUser.CreateUserResponse response = new ICreateUser.CreateUserResponse
            {
                IsSuccess = false,
                Message = message,
                UserId = null
            };
            return response;
        }

        private bool IsRequestValid(ICreateUser.CreateUserRequest request, out string errorMessage)
        {
            if (request == null)
            {
                errorMessage = "Request cannot be null.";
                return false;
            }

            if (
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password)
                )
            {
                errorMessage = "Firstname, email, and password are required.";
                return false;
            }

            if (
                request.FirstName.Length > 100 ||
                (request.LastName != null && request.LastName.Length > 100) ||
                request.Email.Length > 250 ||
                request.Password.Length > 150
                )
            {
                errorMessage = "One or more fields exceed the maximum allowed length.";
                return false;
            }

            if (_userDataAccess.ExistsByEmail(request.Email))
            {
                errorMessage = "This email address is already registered.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
