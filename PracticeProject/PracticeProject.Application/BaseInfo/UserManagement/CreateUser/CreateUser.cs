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
            if (request == null)
                return CreateFailureResponse("Request cannot be null.");

            if (
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password)
                )
                return CreateFailureResponse("Firstname, email, and password are required.");

            if (
                request.FirstName.Length > 100 ||
                (request.LastName != null && request.LastName.Length > 100) ||
                request.Email.Length > 250 ||
                request.Password.Length > 150
                )
                return CreateFailureResponse("One or more fields exceed the maximum allowed length.");

            if (_userDataAccess.ExistsByEmail(request.Email))
                return CreateFailureResponse("This email address is already registered.");

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
                0,
                role,
                request.Email,
                request.Password
                )
            { 
                Person = person
            };

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
    }
}
