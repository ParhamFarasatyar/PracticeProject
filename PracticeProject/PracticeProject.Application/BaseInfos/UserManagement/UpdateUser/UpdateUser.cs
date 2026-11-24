using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System;
using System.Data;

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
            request.Validate();

            User user = userDataAccess.GetById(request.Id);
            user.Person = personDataAccess.GetById(user.PersonId);

            if (user is null)
                throw new AppException($"No user found with ID {request.Id}");

            if (userDataAccess.ExistsByEmail(request.Email, request.Id))
                throw new AppException("Email already exists.");

            user = request.ApplyUpdates(user, passwordHasher);

            userDataAccess.Update(user);
            personDataAccess.Update(user.Person);
            db.SaveChanges();

            return user;
        }
    }
}
