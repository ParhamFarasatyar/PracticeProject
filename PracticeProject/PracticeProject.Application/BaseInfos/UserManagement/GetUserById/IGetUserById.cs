using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetUserById;

public interface IGetUserById
{
    User Execute(int id);
}