using System.Data;
using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.CreateUser;

public class CreateUser(IUserDataAccess userDataAccess, IDb db, IPasswordHasher passwordHasher) : ICreateUser
{
    public User Execute(ICreateUser.Request request)
    {
        var user = ValidateRequest(request);

        userDataAccess.Create(user);
        db.SaveChanges();

        return user;
    }

    private User ValidateRequest(ICreateUser.Request request)
    {
        if (request == null)
            throw new NoNullAllowedException("Request cannot be null");

        if (string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
            throw new AppException("Firstname, email, and password cannot be null");

        if (request.Age < 18 || request.Age > 80)
            throw new AppException("Age value should between 18 and 80.");

        if (request.FirstName.Length > 100 ||
            (request.LastName != null && request.LastName.Length > 100) ||
            request.Email.Length > 250 ||
            request.Password.Length > 150)
            throw new AppException("One or more fields exceed the maximum allowed length.");

        if (userDataAccess.ExistsByEmail(request.Email))
            throw new AppException("This email address is already registered.");

        var person = new Person
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age,
            Gender = request.Gender
        };

        var hashedPassword = passwordHasher.Hash(request.Password);

        var user = new User(
            request.Role,
            request.Email,
            hashedPassword,
            person
        );

        return user;
    }
}