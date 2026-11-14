using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Application.BaseInfo.UserManagement.SearchUsers
{
    public interface ISearchUsers
    {
        IQueryable<User> Execute(Request request);

        public class Request
        {
            public string SearchString { get; set; }
        }
    }
}
