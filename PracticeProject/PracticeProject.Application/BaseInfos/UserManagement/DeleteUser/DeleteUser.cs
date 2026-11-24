using PracticeProject.Domain.BaseInfo.Exceptions;
using System.Data;

namespace PracticeProject.Application.BaseInfos.UserManagement.DeleteUser
{
    public class DeleteUser(IUserDataAccess userDataAccess, IDb db, IPersonDataAccess personDataAccess) : IDeleteUser
    {
        public void Execute(IDeleteUser.Request request)
        {
            request.Validate();

            userDataAccess.Delete(request.UserId);
            personDataAccess.Delete(request.PersonId);
            db.SaveChanges();
        }
    }
}
