namespace PracticeProject.Application.BaseInfo.UserManagement.DeleteUser
{
    public interface IDeleteUser
    {
        DeleteUser.DeleteUserResponse Execute(DeleteUser.DeleteUserRequest request);
    }
}
