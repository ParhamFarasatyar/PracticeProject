using PracticeProject.Domain.BaseInfo.User;
using System;
using System.Data.Entity.Infrastructure;

namespace PracticeProject.Application.BaseInfo.UserManagement.UpdateUser
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IUnitOfWork _unitOfWork;

        public UpdateUser(IUserDataAccess userDataAccess, IUnitOfWork unitOfWork)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IUpdateUser.UpdateUserResponse Execute(IUpdateUser.UpdateUserRequest request)
        {
            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            if (string.IsNullOrWhiteSpace(request.Email)  || string.IsNullOrWhiteSpace(request.Password))
                return CreateFailureResponse("Email and password cannot be null.");

            if (request.Id <= 0)
                return CreateFailureResponse("ID cannot be zero or negative.");

            if (!Enum.IsDefined(typeof(Role), request.Role))
                return CreateFailureResponse("Invalid user role.");

            User user = _userDataAccess.GetById(request.Id);

            if (user is null)
                return CreateFailureResponse($"No user found with ID {request.Id}");

            if (_userDataAccess.ExistsByEmail(request.Email, request.Id))
                return CreateFailureResponse("Email already exists.");

            user.Role = request.Role;
            user.Email = request.Email;
            user.Password = request.Password;

            try
            {
                _userDataAccess.Update(user);
                _unitOfWork.SaveChanges();

                return new IUpdateUser.UpdateUserResponse
                {
                    IsSuccess = true,
                    Message = "User modified successfully",
                    ModifiedUser = user
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                return CreateFailureResponse("The user was modified or deleted by another operation.");
            }
            catch (DbUpdateException)
            {
                return CreateFailureResponse("Could not update user due to a database error.");
            }
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
    }
}
