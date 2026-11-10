namespace PracticeProject.Application.BaseInfo.UserManagement.DeleteUser
{
    public interface IDeleteUser
    {
        DeleteUserResponse Execute(DeleteUserRequest request);

        public class DeleteUserRequest
        {
            public int Id { get; set; }
        }
        public class DeleteUserResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public int? UserId { get; set; }
        }
    }
}
