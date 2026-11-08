namespace PracticeProject.Application.BaseInfo.UserManagement.UpdateUser
{
    public interface IUpdateUser
    {
        UpdateUser.UpdateUserResponse Execute(UpdateUser.UpdateUserRequest request);
    }
}
