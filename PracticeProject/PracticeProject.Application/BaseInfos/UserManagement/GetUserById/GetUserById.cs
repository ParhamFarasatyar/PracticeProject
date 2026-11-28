using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetUserById;

public class GetUserById(IUserDataAccess userDataAccess, IPersonDataAccess personDataAccess) : IGetUserById
{
    public User Execute(int id)
    {
        if (id <= 0)
            throw new AppException("ID cannot be zero or negative.");

        var user = userDataAccess.GetById(id);
        user.Person = personDataAccess.GetById(user.PersonId);

        if (user is null)
            throw new AppException($"No user found with ID {id}");

        return user;
    }
}