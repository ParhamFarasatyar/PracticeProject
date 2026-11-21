using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.LoginUser
{
    public interface ILoginUser
    {
        User Execute(LoginUserRequest request);
        
        class LoginUserRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
