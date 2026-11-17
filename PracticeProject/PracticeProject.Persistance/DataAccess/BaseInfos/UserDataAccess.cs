using PracticeProject.Application;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement;
using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using PracticeProject.Persistance.DataAccess.BaseInfos;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PracticeProject.Persistence.DataAccess
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
            User user = _searchUsers.Execute(new ISearchUsers.Request
            {
                Id = id
            }).SingleOrDefault();

            if (user is null)
                return;

            Person person = _searchPersons.Execute(new ISearchPersons.Request
            {
                Id = user.PersonId
            }).SingleOrDefault();

            if (person is null)
                return;

            _db.Remove<User>(user);
            _db.Remove<Person>(person);
        }

        public IQueryable<User> GetAll() => _searchUsers.Execute();

        public User GetById(int id) => _searchUsers.Execute(new ISearchUsers.Request { Id = id })
            .SingleOrDefault();

        public User GetByEmail(string email) => _searchUsers.Execute(new ISearchUsers.Request
        {
            KeyWord = email
        }).SingleOrDefault();

        public void Update(User user)
        {
            _db.Update<User>(user);
            _db.Update<Person>(user.Person);
        }

        public bool ExistsByEmail(string email) => _searchUsers.Execute(new ISearchUsers.Request 
        { 
            KeyWord = email
        }).Any();

        public bool ExistsByEmail(string email, int excludedUserId) => _searchUsers.Execute(new ISearchUsers.Request
        {
            Id = excludedUserId,
            KeyWord = email
        }).Any(u => u.Id != excludedUserId);
    }
}
