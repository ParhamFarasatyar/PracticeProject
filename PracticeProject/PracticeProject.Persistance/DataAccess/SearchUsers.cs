using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;
using System.Linq;
using PracticeProject.Application.BaseInfo.UserManagement.SearchUsers;
using PracticeProject.Application;
using PracticeProject.Application.BaseInfo;
using System.Data.Entity;

namespace PracticeProject.Persistence.DataAccess
{
    public class SearchUsers : ISearchUsers
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly IDb _db;

        public SearchUsers(IUserDataAccess userDataAccess, IDb db)
        {
            _userDataAccess = userDataAccess;
            _db = db;
        }

        public IQueryable<User> Execute(ISearchUsers.Request request)
        {
            var searchResult = _db.Query<User>().Include(u => u.Person);
            if (request is null)
                return searchResult;

            if (!string.IsNullOrWhiteSpace(request.KeyWord))
                searchResult = _userDataAccess.SearchUsers(request.KeyWord);

            return searchResult;
        }
    }
}
