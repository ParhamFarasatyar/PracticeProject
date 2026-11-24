using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System;
using System.Data;

namespace PracticeProject.Application.BaseInfos.UserManagement.UpdateUser
{
    public static class UpdateUserExtentions
    {
        public static void Validate(this IUpdateUser.Request request)
        {
            if (request is null)
                throw new NoNullAllowedException("Request cannot be null");

            if (request.Age != null)
            {
                if (request.Age < 18 || request.Age > 80)
                    throw new AppException("Age must be higher than 18 and lower than 80");
            }

            if (request.Id <= 0)
                throw new AppException("ID cannot be zero or negative");

            if (request.Role != null)
            {
                if (!Enum.IsDefined(typeof(Role), request.Role))
                    throw new AppException("Invalid user role");
            }

            if (request.Gender != null)
            {
                if (!Enum.IsDefined(typeof(Gender), request.Gender))
                    throw new AppException("Invalid user gender");
            }
        }

        public static User ApplyUpdates(this IUpdateUser.Request request, User user, IPasswordHasher passwordHasher)
        {
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

            return user;
        }
    }
}
