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
            IUserDataAccess.SearchResult result = _searchUsers.Execute(new ISearchUsers.Request
            {
                Id = id,
                PersonRequest = new ISearchPersons.Request()
            }).SingleOrDefault(r => r.User.Id == id);

            if (result is null)
                return;

            _db.Remove<User>(result.User);
            _db.Remove<Person>(result.Person);
        }

        public IQueryable<IUserDataAccess.SearchResult> GetAll() => _searchUsers.Execute(new ISearchUsers.Request 
        { 
            PersonRequest = new ISearchPersons.Request()
        });

        public IUserDataAccess.SearchResult GetById(int id) => _searchUsers.Execute(new ISearchUsers.Request 
        { 
            Id = id,
            PersonRequest = new ISearchPersons.Request()
        }).SingleOrDefault(r => r.User.Id == id);

        public IUserDataAccess.SearchResult GetByEmail(string email) => _searchUsers.Execute(new ISearchUsers.Request
        {
            KeyWord = email,
            PersonRequest = new ISearchPersons.Request()
        }).SingleOrDefault(r => r.User.Email == email);

        public void Update(IUserDataAccess.SearchResult result)
        {
            _db.Update<User>(result.User);
            _db.Update<Person>(result.Person);
        }

        public bool ExistsByEmail(string email) => _searchUsers.Execute(new ISearchUsers.Request 
        { 
            KeyWord = email
        }).Any();

        public bool ExistsByEmail(string email, int excludedUserId) => _searchUsers.Execute(new ISearchUsers.Request
        {
            Id = excludedUserId,
            KeyWord = email
        }).Any(u => u.User.Id != excludedUserId);
    }
}
