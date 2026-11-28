using System.Linq;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;

public static class SearchUserExtensions
{
    public static IQueryable<User> FilterByEmail(this IQueryable<User> items, string keyword)
    {
        return items.Where(r => r.Email.Contains(keyword));
    }

    public static IQueryable<User> FilterByPerson(this IQueryable<User> items, ISearchPersons searchPersons, ISearchPersons.Request personRequest)
    {
        return from user in items 
            join person in searchPersons.Respond(personRequest) 
                on user.PersonId equals person.Id 
            select user;
    }
}