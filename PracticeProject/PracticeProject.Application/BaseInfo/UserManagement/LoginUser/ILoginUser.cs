using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.LoginUser
{
    public interface ILoginUser
    {
        LoginUserResponse Execute(LoginUserRequest request);
        
        class LoginUserRequest
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        class LoginUserResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
        }
    }
}
