using PracticeProject.Application.BaseInfos.PersonManagement;
using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Application.BaseInfos.UserManagement.CreateUser;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PracticeProject.Application.BaseInfos.UserManagement.UpdateUser
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IDb _db;

        private readonly IPasswordHasher _passwordHasher;

        private readonly ISearchPersons _searchPersons;

        public UpdateUser(IUserDataAccess userDataAccess, IDb db, IPasswordHasher passwordHasher, ISearchPersons searchPersons)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _searchPersons = searchPersons ?? throw new ArgumentNullException(nameof(searchPersons));
        }

        public IUpdateUser.Response Execute(IUpdateUser.Request request)
        {
            bool isRequestValid = IsRequestValid(request, out User user, out string errorMessage);

            if (!isRequestValid)
                return CreateFailureResponse(errorMessage);

            string validPassword;
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (_passwordHasher.Verify(request.Password, user.Password))
                    validPassword = user.Password;
                else
                    validPassword = _passwordHasher.Hash(request.Password);
            }
            else
                validPassword = user.Password;

            user.Person.FirstName = request.FirstName ?? user.Person.FirstName;
            user.Person.LastName = request.LastName ?? user.Person.LastName;
            user.Person.Age = request.Age ?? user.Person.Age;
            user.Person.Gender = request.Gender ?? user.Person.Gender;
            user.Role = request.Role ?? user.Role;
            user.Email = request.Email ?? user.Email;
            user.Password = validPassword;

            _userDataAccess.Update(user);
            _db.SaveChanges();

            return new IUpdateUser.Response
            {
                IsSuccess = true,
                Message = "User modified successfully",
                ModifiedUser = user
            };
        }

        private IUpdateUser.Response CreateFailureResponse(string message)
        {
            return new IUpdateUser.Response
            {
                IsSuccess = false,
                Message = message,
                ModifiedUser = null
            };
        }

        private bool IsRequestValid(IUpdateUser.Request request, out User user, out string errorMessage)
        {
            if (request is null)
            {
                user = null;
                errorMessage = "Request cannot be null.";
                return false;
            }

            if (request.Age != null)
            {
                if (request.Age < 18 || request.Age > 80)
                {
                    user = null;
                    errorMessage = "Age must be higher than 18 and lower than 80.";
                    return false;
                }
            }

            if (request.Id <= 0)
            {
                user = null;
                errorMessage = "ID cannot be zero or negative.";
                return false;
            }

            if (request.Role != null)
            {
                if (!Enum.IsDefined(typeof(Role), request.Role))
                {
                    user = null;
                    errorMessage = "Invalid user role.";
                    return false;
                }
            }

            if (request.Gender != null)
            {
                if (!Enum.IsDefined(typeof(Gender), request.Gender))
                {
                    user = null;
                    errorMessage = "Invalid user gender.";
                    return false;
                }
            }

            user = _userDataAccess.GetById(request.Id);

            if (user is null)
            {
                errorMessage = $"No user found with ID {request.Id}";
                return false;
            }

            if (_userDataAccess.ExistsByEmail(request.Email, request.Id))
            {
                errorMessage = "Email already exists.";
                return false;
            }

            user.Person = _searchPersons.Execute(new ISearchPersons.Request
            {
                Id = user.PersonId
            }).SingleOrDefault();

            errorMessage = null;
            return true;
        }
    }
}
