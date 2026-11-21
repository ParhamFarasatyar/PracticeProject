using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.CreateUser
{
    public class CreateUser(IUserDataAccess userDataAccess, IDb db, IPasswordHasher passwordHasher) : ICreateUser
    {
        public ICreateUser.Response Execute(ICreateUser.Request request)
        {
            bool isRequestValid = IsRequestValid(request, out string errorMessage, out User user);

            if (!isRequestValid)
                return CreateFailureResponse(errorMessage);

            userDataAccess.Create(user);
            db.SaveChanges();

            ICreateUser.Response response = new ICreateUser.Response
            {
                IsSuccess = true,
                Message = "User created successfully.",
                User = user
            };
            return response;
        }

        private ICreateUser.Response CreateFailureResponse(string message)
        {
            ICreateUser.Response response = new ICreateUser.Response
            {
                IsSuccess = false,
                Message = message,
                User = null
            };
            return response;
        }

        private bool IsRequestValid(ICreateUser.Request request, out string errorMessage, out User user)
        {
            if (request == null)
            {
                errorMessage = "Request cannot be null.";
                user = null;
                return false;
            }

            if (
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password)
                )
            {
                errorMessage = "Firstname, email, and password are required.";
                user = null;
                return false;
            }

            if (request.Age < 18 || request.Age > 80)
            {
                errorMessage = "Age value should between 18 and 80.";
                user = null;
                return false;
            }

            if (
                request.FirstName.Length > 100 ||
                (request.LastName != null && request.LastName.Length > 100) ||
                request.Email.Length > 250 ||
                request.Password.Length > 150
                )
            {
                errorMessage = "One or more fields exceed the maximum allowed length.";
                user = null;
                return false;
            }

            if (userDataAccess.ExistsByEmail(request.Email))
            {
                errorMessage = "This email address is already registered.";
                user = null;
                return false;
            }

            Person person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Gender = request.Gender
            };

            string hashedPassword = passwordHasher.Hash(request.Password);

            user = new User(
                request.Role,
                request.Email,
                hashedPassword,
                person
                );

            errorMessage = null;
            return true;
        }
    }
}
