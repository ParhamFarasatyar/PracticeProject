using PracticeProject.Application.UserManagement.CreateUser;

namespace PracticeProject.Application.BaseInfo.UserManagement.CreateUser
{
    public interface ICreateUser
    {
        CreateUserResponse Execute(CreateUserRequest request);
    }
}
