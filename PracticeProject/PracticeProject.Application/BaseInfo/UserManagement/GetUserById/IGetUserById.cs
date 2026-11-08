namespace PracticeProject.Application.BaseInfo.UserManagement.GetUserById
{
    public interface IGetUserById
    {
        GetUserById.GetUserByIdResponse Execute(GetUserById.GetUserByIdRequest request);
    }
}
