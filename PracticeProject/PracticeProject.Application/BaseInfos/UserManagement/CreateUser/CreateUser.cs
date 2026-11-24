using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System.Data;

namespace PracticeProject.Application.BaseInfos.UserManagement.CreateUser
{
    public class CreateUser(IUserDataAccess userDataAccess, IDb db, IPasswordHasher passwordHasher) : ICreateUser
    {
        public User Execute(ICreateUser.Request request)
        {
            User user = ValidateRequest(request);

            userDataAccess.Create(user);
            db.SaveChanges();

            return user;
        }

        private User ValidateRequest(ICreateUser.Request request)
        {
            request.Validate();

            if (userDataAccess.ExistsByEmail(request.Email))
                throw new AppException("This email address is already registered.");

            Person person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Gender = request.Gender
            };

            string hashedPassword = passwordHasher.Hash(request.Password);

            User user = new User(
                request.Role,
                request.Email,
                hashedPassword,
                person
                );

            return user;
        }
    }
}
