using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.LoginUser
{
    public class LoginUser(IUserDataAccess userDataAccess, IPasswordHasher passwordHasher) : ILoginUser
    {
        public ILoginUser.Response Execute(ILoginUser.LoginUserRequest request)
        {
            bool isRequestValid = IsRequestValid(request, out User user, out string errorMessage);
            if (!isRequestValid)
                return CreateFailureResponse(errorMessage);

            return new ILoginUser.Response
            {
                IsSuccess = true,
                Message = "User logged in successfully.",
                User = user
            };
        }

        private bool IsRequestValid(ILoginUser.LoginUserRequest request, out User user, out string errorMessage)
        {
            if (request is null)
            {
                errorMessage = "Request cannot be null.";
                user = null;
                return false;
            }
            
            if (request.Email is null || request.Password is null)
            {
                errorMessage = "Email or password cannot be null.";
                user = null;
                return false;
            }


            user = userDataAccess.GetByEmail(request.Email);
            if (user is null || !passwordHasher.Verify(request.Password, user.Password))
            {
                errorMessage = "Incorrect email or password.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        private ILoginUser.Response CreateFailureResponse(string message)
        {
            return new ILoginUser.Response
            {
                IsSuccess = false,
                Message = message,
                User = null
            };
        }
    }
}
