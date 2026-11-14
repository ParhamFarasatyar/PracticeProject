using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.LoginUser
{
    public interface ILoginUser
    {
        Response Execute(LoginUserRequest request);
        
        class LoginUserRequest
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        class Response
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
        }
    }
}
