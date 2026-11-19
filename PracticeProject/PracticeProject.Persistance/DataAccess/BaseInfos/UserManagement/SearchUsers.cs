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
using static PracticeProject.Application.BaseInfos.IUserDataAccess;

namespace PracticeProject.Persistence.DataAccess.BaseInfos.UserManagement
{
    public class SearchUsers(IDb db, ISearchPersons searchPersons) : ISearchUsers
    {
        public IQueryable<SearchResult> Execute(ISearchUsers.Request request = null)
        {
            var users = db.Query<User>();

            var people = searchPersons.Execute();

            var items =
                from user in users
                join person in people
                    on user.PersonId equals person.Id
                select new SearchResult
                {
                    User = user,
                    Person = person
                };

            return ValidateRequest(request, ref items);
        }

        private IQueryable<SearchResult> ValidateRequest
            (ISearchUsers.Request request, ref IQueryable<SearchResult> items)
        {
            if (request is null)
                return items;

            if (request.Id.HasValue)
                items = items.Where(r => r.User.Id == request.Id);

            if (!string.IsNullOrWhiteSpace(request.KeyWord))
            {
                items = items.Where(r =>
                    r.User.Email.Contains(request.KeyWord) ||
                    r.Person.FirstName.Contains(request.KeyWord) ||
                    r.Person.LastName.Contains(request.KeyWord));
            }

            return items;
        }
    }
}
