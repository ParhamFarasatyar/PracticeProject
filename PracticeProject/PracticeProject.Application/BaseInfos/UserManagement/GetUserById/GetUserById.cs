using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.Users;
using System.Data;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetUserById
{
    public class GetUserById(IUserDataAccess userDataAccess, IPersonDataAccess personDataAccess) : IGetUserById
    {
        public User Execute(IGetUserById.Request request)
        {
            if (request is null)
                throw new NoNullAllowedException("Request cannot be null.");

            if (request.Id <= 0)
                throw new AppException("ID cannot be zero or negative.");

            User user = userDataAccess.GetById(request.Id);
            user.Person = personDataAccess.GetById(user.PersonId);

            if (user is null)
                throw new AppException($"No user found with ID {request.Id}");

            return user;
        }
    }
}
