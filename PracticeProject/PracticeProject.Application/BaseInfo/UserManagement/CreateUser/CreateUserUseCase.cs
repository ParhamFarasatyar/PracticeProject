using PracticeProject.Application.Interfaces;
using PracticeProject.Domain.BaseInfo.User;
using System;

namespace PracticeProject.Application.UserUseCases.CreateUser
{
    public class CreateUserUseCase
    {
        private readonly IUserDataAccess _userDataAccess;

        public CreateUserUseCase(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
        }

        public CreateUserResponse Execute(CreateUserRequest request)
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

            Role role = Role.User;
            if (string.Equals(request.FirstName, "Parham", StringComparison.OrdinalIgnoreCase))
            {
                role = Role.Admin;
            }

            var user = new User(
                0,
                role,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
                );

            _userDataAccess.Create(user);

            CreateUserResponse response = new CreateUserResponse
            {
                IsSuccess = true,
                Message = "User created successfully.",
                UserId = user.Id
            };
            return response;
        }

        private CreateUserResponse CreateFailureResponse(string message)
        {
            CreateUserResponse response = new CreateUserResponse
            {
                IsSuccess = false,
                Message = message,
                UserId = null
            };
            return response;
        }
    }
}
