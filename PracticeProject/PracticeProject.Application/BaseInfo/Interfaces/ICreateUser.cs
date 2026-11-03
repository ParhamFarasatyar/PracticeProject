using PracticeProject.Application.UserManagement.CreateUser;

namespace PracticeProject.Application.BaseInfo.Interfaces
{
    public interface ICreateUser
    {
        CreateUserResponse Execute(CreateUserRequest request);
    }
}
