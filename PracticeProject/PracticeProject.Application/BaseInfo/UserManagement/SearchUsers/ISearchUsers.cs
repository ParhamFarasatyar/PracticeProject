using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;

namespace PracticeProject.Application.BaseInfo.UserManagement.SearchUsers
{
    public interface ISearchUsers
    {
        SearchUsersResponse Execute(SearchUsersRequest request);

        public class SearchUsersRequest
        {
            public string SearchString { get; set; }
        }

        public class SearchUsersResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public List<User> SearchResult { get; set; }
        }
    }
}
