namespace PracticeProject.Application.BaseInfos.UserManagement.DeleteUser
{
    public class DeleteUser(IUserDataAccess userDataAccess, IDb db) : IDeleteUser
    {
        public IDeleteUser.Response Execute(IDeleteUser.Request request)
        {
            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            if (request.Id <= 0)
                return CreateFailureResponse("ID cannot be zero or negative.");

            userDataAccess.Delete(request.Id);
            db.SaveChanges();

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
