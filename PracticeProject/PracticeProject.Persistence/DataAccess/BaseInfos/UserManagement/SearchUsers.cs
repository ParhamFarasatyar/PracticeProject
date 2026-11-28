using System.Linq;
using PracticeProject.Application;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Persistence.DataAccess.BaseInfos.UserManagement;

public class SearchUsers(IDb db, ISearchPersons searchPersons) : ISearchUsers
{
    public IQueryable<User> Respond(ISearchUsers.Request request = null)
    {
        return Respond(request, db.Query<User>());
    }

    private IQueryable<User> Respond(ISearchUsers.Request request, IQueryable<User> items)
    {
        if (request is null)
            return items;

        if (request.Id.HasValue)
            items = items.FilterById(request.Id);

        if (!string.IsNullOrWhiteSpace(request.KeyWord))
            items = items.FilterByEmail(request.KeyWord);

        if (request.PersonRequest is not null)
            items = items.FilterByPerson(searchPersons.Respond(request.PersonRequest));

        return items;
    }
}