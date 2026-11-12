using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.LoginUser
{
    public class LoginUser : ILoginUser
    {
        private readonly IUserDataAccess _userDataAccess;

        public LoginUser(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public ILoginUser.LoginUserResponse Execute(ILoginUser.LoginUserRequest request)
        {
            bool isRequestValid = IsRequestValid(request, out User user, out string errorMessage);
            if (!isRequestValid)
                return CreateFailureResponse(errorMessage);

            return new ILoginUser.LoginUserResponse
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

            user = _userDataAccess.GetByEmail(request.Email, request.Password);
            if (user is null)
            {
                errorMessage = "Incorrect email or password.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        private ILoginUser.LoginUserResponse CreateFailureResponse(string message)
        {
            return new ILoginUser.LoginUserResponse
            {
                IsSuccess = false,
                Message = message,
                User = null
            };
        }
    }
}
