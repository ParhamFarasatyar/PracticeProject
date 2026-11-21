using PracticeProject.Application;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System.Linq;

namespace PracticeProject.Persistence.DataAccess.BaseInfos.UserManagement
{
    public class UserDataAccess(IDb db, ISearchUsers searchUsers, IPersonDataAccess personDataAccess) : IUserDataAccess
    {
        public void Create(User user) => db.Add<User>(user);

        public void Delete(int id)
        {
            User user = GetById(id);

            if (user is null)
                return;

            db.Remove<User>(user);
            personDataAccess.Delete(user.PersonId);
        }

        public IQueryable<User> GetAll() => searchUsers.Respond(new ISearchUsers.Request
        {
            PersonRequest = new ISearchPersons.Request()
        });

        public User GetById(int id)
        {
            return searchUsers.Respond(new ISearchUsers.Request
            {
                Id = id,
                PersonRequest = new ISearchPersons.Request()
            }).SingleOrDefault(u => u.Id == id);
        }

        public User GetByEmail(string email) => searchUsers.Respond(new ISearchUsers.Request
        {
            KeyWord = email,
            PersonRequest = new ISearchPersons.Request()
        }).SingleOrDefault(r => r.Email == email);

        public void Update(User user)
        {
            db.Update<User>(user);
            personDataAccess.Update(user.Person);
        }

        public bool ExistsByEmail(string email) => searchUsers.Respond(new ISearchUsers.Request
        {
            KeyWord = email
        }).Any();

        public bool ExistsByEmail(string email, int excludedUserId) => searchUsers.Respond(new ISearchUsers.Request
        {
            Id = excludedUserId,
            KeyWord = email
        }).Any(u => u.Id != excludedUserId);
    }
}
