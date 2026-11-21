using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.CreateUser
{
    public class CreateUser(IUserDataAccess userDataAccess, IDb db, IPasswordHasher passwordHasher) : ICreateUser
    {
        public ICreateUser.Response Execute(ICreateUser.Request request)
        {
            User user = ValidateRequest(request);

            userDataAccess.Create(user);
            db.SaveChanges();
            
            return new ICreateUser.Response
            {
                IsSuccess = true,
                User = user
            };
        }

        private User ValidateRequest(ICreateUser.Request request)
        {
            if (request == null)
                throw new NoNullAllowedException("Request cannot be null");

            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
                throw new NoNullAllowedException("Firstname, email, and password cannot be null");

            if (request.Age < 18 || request.Age > 80)
                throw new ValueOutOfRangeException("Age value should between 18 and 80.");

            if (request.FirstName.Length > 100 ||
                (request.LastName != null && request.LastName.Length > 100) ||
                request.Email.Length > 250 ||
                request.Password.Length > 150)
                throw new ExceedFieldLengthException();

            if (userDataAccess.ExistsByEmail(request.Email))
                throw new DuplicateEmailException();

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
