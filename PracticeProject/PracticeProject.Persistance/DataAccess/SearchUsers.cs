using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;
using System.Linq;
using PracticeProject.Application.BaseInfo;
using PracticeProject.Application.BaseInfo.UserManagement.SearchUsers;

namespace PracticeProject.Persistence.DataAccess
{
    public class SearchUsers : ISearchUsers
    {
        private readonly IUserDataAccess _userDataAccess;

        public SearchUsers(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public IQueryable<User> Execute(ISearchUsers.Request request)
        {
            if (request is null)
                return null; 

            if (string.IsNullOrWhiteSpace(request.SearchString))
                return _userDataAccess.GetAll();
            else 
                return _userDataAccess.SearchUsers(request.SearchString);
        }
    }
}
