using PracticeProject.Application;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using PracticeProject.Persistance.DataAccess.BaseInfos.Security.BaseInfos.PersonManagement;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PracticeProject.Persistence.DataAccess.BaseInfos.UserManagement
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IDb _db;
        private readonly ISearchUsers _searchUsers;
        private readonly ISearchPersons _searchPersons;

        public UserDataAccess(IDb db, ISearchUsers searchUsers, ISearchPersons searchPersons)
        {
            _db = db;
            _searchUsers = searchUsers;
            _searchPersons = searchPersons;
        }

        public void Create(User user) => _db.Add<User>(user);

        public void Delete(int id)
        {
            User user = GetById(id);

            if (user is null)
                return;

            _db.Remove<User>(user);
            _db.Remove<Person>(user.Person);
        }

        public IQueryable<User> GetAll() => _searchUsers.Respond(new ISearchUsers.Request 
        { 
            PersonRequest = new ISearchPersons.Request()
        });

        public User GetById(int id) => _searchUsers.Respond(new ISearchUsers.Request 
        { 
            Id = id,
            PersonRequest = new ISearchPersons.Request()
        }).SingleOrDefault(u => u.Id == id);

        public User GetByEmail(string email) => _searchUsers.Respond(new ISearchUsers.Request
        {
            KeyWord = email,
            PersonRequest = new ISearchPersons.Request()
        }).SingleOrDefault(r => r.Email == email);

        public void Update(User user)
        {
            _db.Update<User>(user);
            _db.Update<Person>(user.Person);
        }

        public bool ExistsByEmail(string email) => _searchUsers.Respond(new ISearchUsers.Request 
        { 
            KeyWord = email
        }).Any();

        public bool ExistsByEmail(string email, int excludedUserId) => _searchUsers.Respond(new ISearchUsers.Request
        {
            Id = excludedUserId,
            KeyWord = email
        }).Any(u => u.Id != excludedUserId);
    }
}
