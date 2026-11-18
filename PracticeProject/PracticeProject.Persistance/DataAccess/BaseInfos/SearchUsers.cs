using PracticeProject.Application;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPeople;
using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfos.Users;
using PracticeProject.Persistance.DataAccess.BaseInfos.PersonManagement;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PracticeProject.Persistence.DataAccess
{
    public class SearchUsers(IDb db, ISearchPeople searchPersons) : ISearchUsers
    {
        public IQueryable<User> Execute(ISearchUsers.Request request = null)
        {
            var items = db.Query<User>();
            return ValidateRequest(request, ref items);
        }
        
        private IQueryable<User> ValidateRequest(ISearchUsers.Request request, ref IQueryable<User> items)
        {
            if (request is null)
                return items;

            if (request.Id.HasValue)
                items = items.Where(i => i.Id == request.Id);

            if (!string.IsNullOrWhiteSpace(request.KeyWord))
                items = items.Where(i => i.Email.Contains(request.KeyWord));

            if (request.Person is not null)
            {
                var personIds = searchPersons.Execute(new ISearchPeople.Request
                {
                    Id = request.Person.Id,
                    Keyword = request.KeyWord
                }).Select(p => p.Id);

                items = items.Where(u => personIds.Contains(u.PersonId));
            }

            return items;
        }
    }
}
