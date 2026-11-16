using PracticeProject.Application.BaseInfo.Security;
using PracticeProject.Domain.BaseInfo.People;
using PracticeProject.Domain.BaseInfo.Users;
using System;

namespace PracticeProject.Application.BaseInfo.UserManagement.CreateUser
{
    public class CreateUser : ICreateUser
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IDb _db;

        private readonly IPasswordHasher _passwordHasher;

        public CreateUser(IUserDataAccess userDataAccess, IDb db, IPasswordHasher passwordHasher)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public ICreateUser.Response Execute(ICreateUser.Request request)
        {
            bool isRequestValid = IsRequestValid(request, out string errorMessage, out User user);

            if (!isRequestValid)
                return CreateFailureResponse(errorMessage);

            _userDataAccess.Create(user);
            _db.SaveChanges();

            ICreateUser.Response response = new ICreateUser.Response
            {
                IsSuccess = true,
                Message = "User created successfully.",
                User = user
            };
            return response;
        }

        private ICreateUser.Response CreateFailureResponse(string message)
        {
            ICreateUser.Response response = new ICreateUser.Response
            {
                IsSuccess = false,
                Message = message,
                User = null
            };
            return response;
        }

        private bool IsRequestValid(ICreateUser.Request request, out string errorMessage, out User user)
        {
            if (request == null)
            {
                errorMessage = "Request cannot be null.";
                user = null;
                return false;
            }

            if (
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password)
                )
            {
                errorMessage = "Firstname, email, and password are required.";
                user = null;
                return false;
            }

            if (request.Age < 18 || request.Age > 80)
            {
                errorMessage = "Age value should between 18 and 80.";
                user = null;
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
                user = null;
                return false;
            }

            if (_userDataAccess.ExistsByEmail(request.Email))
            {
                errorMessage = "This email address is already registered.";
                user = null;
                return false;
            }

            Person person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Gender = request.Gender
            };

            string hashedPassword = _passwordHasher.Hash(request.Password);

            user = new User(
                request.Role,
                request.Email,
                hashedPassword,
                person
                );

            errorMessage = null;
            return true;
        }
    }
}
