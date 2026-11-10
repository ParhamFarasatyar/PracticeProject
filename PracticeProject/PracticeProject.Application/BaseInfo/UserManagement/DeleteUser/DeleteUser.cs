using System;
using System.Data.Entity.Infrastructure;

namespace PracticeProject.Application.BaseInfo.UserManagement.DeleteUser
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IDb _db;

        public DeleteUser(IUserDataAccess userDataAccess, IDb db)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _db = db ?? throw new ArgumentNullException(nameof(db));
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
                _db.SaveChanges();

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
