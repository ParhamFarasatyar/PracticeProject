using PracticeProject.Domain.BaseInfo.Exceptions;

namespace PracticeProject.Application.BaseInfos.UserManagement.DeleteUser
{
    public class DeleteUser(IUserDataAccess userDataAccess, IDb db) : IDeleteUser
    {
        public void Execute(IDeleteUser.Request request)
        {
            if (request is null)
                throw new NoNullAllowedException("Request cannot be null");

            if (request.Id <= 0)
                throw new InvalidValueException("ID cannot be zero or negative.");

            userDataAccess.Delete(request.Id);
            db.SaveChanges();
        }
    }
}
