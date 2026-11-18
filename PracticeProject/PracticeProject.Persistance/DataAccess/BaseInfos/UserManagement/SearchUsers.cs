using PracticeProject.Application;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPeople;
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
    public class SearchUsers(IDb db, ISearchPeople searchPeople) : ISearchUsers
    {
        public IQueryable<SearchResult> Execute(ISearchUsers.Request request = null)
        {
            var items = db.Query<User>().Select(user =>
                new SearchResult
                {
                    User = user,
                    Person = null
                });
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
                items = items.Where(i => i.User.Email.Contains(request.KeyWord));

            if (request.PersonRequest is not null)
            {
                IQueryable<Person> people = searchPeople.Execute(request.PersonRequest);

                items =
                    from user in items
                    join person in people
                    on user.User.PersonId equals person.Id into personGroup
                    from person in personGroup.DefaultIfEmpty()
                    where person != null
                    select new SearchResult
                    {
                        User = user.User,
                        Person = person
                    };
            }

            return items;
        }
    }
}
