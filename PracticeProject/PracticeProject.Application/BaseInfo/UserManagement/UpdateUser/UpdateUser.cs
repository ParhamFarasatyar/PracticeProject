using PracticeProject.Application.BaseInfo.UserManagement.CreateUser;
using PracticeProject.Domain.BaseInfo.Users;
using System;
using System.Data.Entity.Infrastructure;

namespace PracticeProject.Application.BaseInfo.UserManagement.UpdateUser
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IDb _db;

        public UpdateUser(IUserDataAccess userDataAccess, IDb db)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IUpdateUser.UpdateUserResponse Execute(IUpdateUser.UpdateUserRequest request)
        {
            bool isRequestValid = IsRequestValid(request, out User user, out string errorMessage);

            if (!isRequestValid)
                return CreateFailureResponse(errorMessage);

            user.Role = request.Role;
            user.Email = request.Email;
            user.Password = request.Password;

            _userDataAccess.Update(user);
            _db.SaveChanges();

            return new IUpdateUser.UpdateUserResponse
            {
                IsSuccess = true,
                Message = "User modified successfully",
                ModifiedUser = user
            };
        }

        private IUpdateUser.UpdateUserResponse CreateFailureResponse(string message)
        {
            return new IUpdateUser.UpdateUserResponse
            {
                IsSuccess = false,
                Message = message,
                ModifiedUser = null
            };
        }

        private bool IsRequestValid(IUpdateUser.UpdateUserRequest request, out User user, out string errorMessage)
        {
            if (request is null)
            {
                user = null;
                errorMessage = "Request cannot be null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                user = null;
                errorMessage = "Email and password cannot be null.";
                return false;
            }

            if (request.Id <= 0)
            {
                user = null;
                errorMessage = "ID cannot be zero or negative.";
                return false;
            }

            if (!Enum.IsDefined(typeof(Role), request.Role))
            {
                user = null;
                errorMessage = "Invalid user role.";
                return false;
            }

            user = _userDataAccess.GetById(request.Id);

            if (user is null)
            {
                errorMessage = $"No user found with ID {request.Id}";
                return false;
            }

            if (_userDataAccess.ExistsByEmail(request.Email, request.Id))
            {
                errorMessage = "Email already exists.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
