using System;
using System.Data.Entity.Infrastructure;

namespace PracticeProject.Application.BaseInfo.UserManagement.DeleteUser
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteUser(IUserDataAccess userDataAccess, IUnitOfWork unitOfWork)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IDeleteUser.DeleteUserResponse Execute(IDeleteUser.DeleteUserRequest request)
        {
            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            if (request.Id <= 0)
                return CreateFailureResponse("ID cannot be zero or negative.");

            try
            {
                _userDataAccess.Delete(request.Id);
                _unitOfWork.SaveChanges();

                return new IDeleteUser.DeleteUserResponse
                {
                    IsSuccess = true,
                    Message = "User deleted successfully.",
                    UserId = request.Id
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                return CreateFailureResponse("The user was modified or deleted by another operation.");
            }
            catch (DbUpdateException)
            {
                return CreateFailureResponse("Could not delete user due to a database error.");
            }
        }

        private IDeleteUser.DeleteUserResponse CreateFailureResponse(string message)
        {
            return new IDeleteUser.DeleteUserResponse
            {
                IsSuccess = false,
                Message = message,
                UserId = null
            };
        }
    }
}
