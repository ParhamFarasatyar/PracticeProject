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

        public IDeleteUser.Response Execute(IDeleteUser.Request request)
        {
            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            if (request.Id <= 0)
                return CreateFailureResponse("ID cannot be zero or negative.");

            _userDataAccess.Delete(request.Id);
            _db.SaveChanges();

            return new IDeleteUser.Response
            {
                IsSuccess = true,
                Message = "User deleted successfully.",
                UserId = request.Id
            };
        }

        private IDeleteUser.Response CreateFailureResponse(string message)
        {
            return new IDeleteUser.Response
            {
                IsSuccess = false,
                Message = message,
                UserId = null
            };
        }
    }
}
