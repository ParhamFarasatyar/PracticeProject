namespace PracticeProject.Application.BaseInfo.UserManagement.CreateUser
{
    public interface ICreateUser
    {
        CreateUser.CreateUserResponse Execute(CreateUser.CreateUserRequest request);
    }
}
