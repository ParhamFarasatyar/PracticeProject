using System.Data;
using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.LoginUser;

public class LoginUser(IUserDataAccess userDataAccess, IPasswordHasher passwordHasher) : ILoginUser
{
    public User Execute(ILoginUser.LoginUserRequest request)
    {
        return ValidateRequest(request);
    }

    private User ValidateRequest(ILoginUser.LoginUserRequest request)
    {
        if (request is null)
            throw new NoNullAllowedException("Request cannot be null");

        if (request.Email is null || request.Password is null)
            throw new AppException("Email or password cannot be null");

        var user = userDataAccess.GetByEmail(request.Email);
        if (user is null || !passwordHasher.Verify(request.Password, user.Password))
            throw new AppException("Incorrect email or password");

        return user;
    }
}