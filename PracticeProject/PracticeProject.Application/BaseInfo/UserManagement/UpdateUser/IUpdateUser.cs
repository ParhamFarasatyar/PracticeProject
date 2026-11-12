using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.UpdateUser
{
    public interface IUpdateUser
    {
        UpdateUserResponse Execute(UpdateUserRequest request);

        public class UpdateUserRequest
        {
            public int Id { get; set; }
            public Role Role { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UpdateUserResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User ModifiedUser { get; set; }
        }
    }
}
