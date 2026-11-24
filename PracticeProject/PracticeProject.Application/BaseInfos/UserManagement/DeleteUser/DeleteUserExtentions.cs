using PracticeProject.Domain.BaseInfo.Exceptions;
using System.Data;

namespace PracticeProject.Application.BaseInfos.UserManagement.DeleteUser
{
    public static class DeleteUserExtentions
    {
        public static void Validate(this IDeleteUser.Request request)
        {
            if (request is null)
                throw new NoNullAllowedException("Request cannot be null");

            if (request.UserId <= 0 || request.PersonId <= 0)
                throw new AppException("User ID and Person ID cannot be zero or negative.");
        }
    }
}
