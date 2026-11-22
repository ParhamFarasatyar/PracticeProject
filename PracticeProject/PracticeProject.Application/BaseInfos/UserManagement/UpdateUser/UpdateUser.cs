using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System;

namespace PracticeProject.Application.BaseInfos.UserManagement.UpdateUser
{
    public class UpdateUser(
        IUserDataAccess userDataAccess,
        IDb db, 
        IPasswordHasher passwordHasher,
        IPersonDataAccess personDataAccess
        ) : IUpdateUser
    {
        public User Execute(IUpdateUser.Request request)
        {
            User user = ValidateRequest(request);

            string validPassword;
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (passwordHasher.Verify(request.Password, user.Password))
                    validPassword = user.Password;
                else
                    validPassword = passwordHasher.Hash(request.Password);
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

            userDataAccess.Update(user);
            personDataAccess.Update(user.Person);
            db.SaveChanges();

            return user;
        }

        private User ValidateRequest(IUpdateUser.Request request)
        {
            if (request is null)
                throw new NoNullAllowedException("Request cannot be null");

            if (request.Age != null)
            {
                if (request.Age < 18 || request.Age > 80)
                    throw new ValueOutOfRangeException("Age must be higher than 18 and lower than 80");
            }

            if (request.Id <= 0)
                throw new InvalidValueException("ID cannot be zero or negative");

            if (request.Role != null)
            {
                if (!Enum.IsDefined(typeof(Role), request.Role))
                    throw new InvalidValueException("Invalid user role");
            }

            if (request.Gender != null)
            {
                if (!Enum.IsDefined(typeof(Gender), request.Gender))
                    throw new InvalidValueException("Invalid user gender");
            }

            User user = userDataAccess.GetById(request.Id);
            user.Person = personDataAccess.GetById(user.PersonId);

            if (user is null)
                throw new UserNotFoundException($"No user found with ID {request.Id}");

            if (userDataAccess.ExistsByEmail(request.Email, request.Id))
                throw new DuplicateEmailException("Email already exists.");

            return user;
        }
    }
}
