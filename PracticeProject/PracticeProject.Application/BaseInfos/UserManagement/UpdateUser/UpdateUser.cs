using PracticeProject.Application.BaseInfos.PersonManagement.SearchPeople;
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

        private readonly ISearchPeople _searchPersons;

        public UpdateUser(IUserDataAccess userDataAccess, IDb db, IPasswordHasher passwordHasher, ISearchPeople searchPersons)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _searchPersons = searchPersons ?? throw new ArgumentNullException(nameof(searchPersons));
        }

        public IUpdateUser.Response Execute(IUpdateUser.Request request)
        {
            bool isRequestValid = IsRequestValid(request, out IUserDataAccess.SearchResult result, out string errorMessage);

            if (!isRequestValid)
                return CreateFailureResponse(errorMessage);

            string validPassword;
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (_passwordHasher.Verify(request.Password, result.User.Password))
                    validPassword = result.User.Password;
                else
                    validPassword = _passwordHasher.Hash(request.Password);
            }
            else
                validPassword = result.User.Password;

            result.Person.FirstName = request.FirstName ?? result.Person.FirstName;
            result.Person.LastName = request.LastName ?? result.Person.LastName;
            result.Person.Age = request.Age ?? result.Person.Age;
            result.Person.Gender = request.Gender ?? result.Person.Gender;
            result.User.Role = request.Role ?? result.User.Role;
            result.User.Email = request.Email ?? result.User.Email;
            result.User.Password = validPassword;

            _userDataAccess.Update(result);
            _db.SaveChanges();

            return new IUpdateUser.Response
            {
                IsSuccess = true,
                Message = "User modified successfully",
                ModifyResult = result
            };
        }

        private IUpdateUser.Response CreateFailureResponse(string message)
        {
            return new IUpdateUser.Response
            {
                IsSuccess = false,
                Message = message,
                ModifyResult = null
            };
        }

        private bool IsRequestValid(IUpdateUser.Request request, out IUserDataAccess.SearchResult result, out string errorMessage)
        {
            if (request is null)
            {
                result = null;
                errorMessage = "Request cannot be null.";
                return false;
            }

            if (request.Age != null)
            {
                if (request.Age < 18 || request.Age > 80)
                {
                    result = null;
                    errorMessage = "Age must be higher than 18 and lower than 80.";
                    return false;
                }
            }

            if (request.Id <= 0)
            {
                result = null;
                errorMessage = "ID cannot be zero or negative.";
                return false;
            }

            if (request.Role != null)
            {
                if (!Enum.IsDefined(typeof(Role), request.Role))
                {
                    result = null;
                    errorMessage = "Invalid user role.";
                    return false;
                }
            }

            if (request.Gender != null)
            {
                if (!Enum.IsDefined(typeof(Gender), request.Gender))
                {
                    result = null;
                    errorMessage = "Invalid user gender.";
                    return false;
                }
            }

            result = _userDataAccess.GetById(request.Id);

            if (result is null || result.User is null || result.Person is null)
            {
                errorMessage = $"No user found with ID {request.Id}";
                return false;
            }

            if (_userDataAccess.ExistsByEmail(request.Email, request.Id))
            {
                errorMessage = "Email already exists.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
