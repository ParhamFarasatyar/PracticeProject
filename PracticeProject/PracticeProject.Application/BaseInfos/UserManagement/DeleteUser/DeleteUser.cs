using System.Data;
using PracticeProject.Domain.BaseInfo.Exceptions;

namespace PracticeProject.Application.BaseInfos.UserManagement.DeleteUser;

public class DeleteUser(IUserDataAccess userDataAccess, IDb db, IPersonDataAccess personDataAccess) : IDeleteUser
{
    public void Execute(IDeleteUser.Request request)
    {
        if (request is null)
            throw new NoNullAllowedException("Request cannot be null");

        if (request.UserId <= 0 || request.PersonId <= 0)
            throw new AppException("User ID and Person ID cannot be zero or negative.");

        userDataAccess.Delete(request.UserId);
        personDataAccess.Delete(request.PersonId);
        db.SaveChanges();
    }
}