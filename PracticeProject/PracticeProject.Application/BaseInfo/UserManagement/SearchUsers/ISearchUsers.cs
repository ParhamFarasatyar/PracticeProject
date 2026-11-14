using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Application.BaseInfo.UserManagement.SearchUsers
{
    public interface ISearchUsers
    {
        IQueryable<User> Execute(SearchUsersRequest request);

        public class SearchUsersRequest
        {
            public string SearchString { get; set; }
        }
    }
}
