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
    public class SearchUsers(IDb db, ISearchPersons searchPersons) : ISearchUsers
    {
        public IQueryable<User> Respond(ISearchUsers.Request request = null) => Respond(request, db.Query<User>());

        private IQueryable<User> Respond(ISearchUsers.Request request, IQueryable<User> items)
        {
            if (request is null)
                return items;

            if (request.Id.HasValue)
                items = items.Where(r => r.Id == request.Id);

            if (!string.IsNullOrWhiteSpace(request.KeyWord))
                items = items.Where(r => r.Email.Contains(request.KeyWord));

            if (request.PersonRequest is not null)
            {
                items =
                    from user in items
                    join person in searchPersons.Execute(request.PersonRequest)
                        on user.PersonId equals person.Id
                    select user;
            }

            return items;
        }
    }
}
