using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfos.Users;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetUsersPage
{
    public interface IGetUsersPage
    {
        Response Respond(Request request);

        public class Request
        {
            public int PageNumber { get; set; }
            public ISearchUsers.Request SearchRequest { get; set; }
        }

        public class Response
        {
            public IQueryable<User> Users { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public int CurrentPage { get; set; }
        }
    }
}
