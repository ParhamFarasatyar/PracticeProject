using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Application.BaseInfos.UserManagement.SearchUsers
{
    public interface ISearchUsers
    {
        IQueryable<User> Respond(Request request = null);

        public class Request
        {
            public int? Id { get; set; }
            public string KeyWord { get; set; }
            public ISearchPersons.Request PersonRequest { get; set; }
        }
    }
}
