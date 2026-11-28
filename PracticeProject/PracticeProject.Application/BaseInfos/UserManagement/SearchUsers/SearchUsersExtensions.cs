using System.Linq;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;

public static class SearchUsersExtensions
{
    public static IQueryable<User> FilterById(this IQueryable<User> items, int? id)
    {
        return items.Where(r => r.Id == id);
    }

    public static IQueryable<User> FilterByEmail(this IQueryable<User> items, string keyword)
    {
        return items.Where(r => r.Email.Contains(keyword));
    }

    public static IQueryable<User> FilterByPerson(this IQueryable<User> items, IQueryable<Person> persons)
    {
        return from user in items
            join person in persons
                on user.PersonId equals person.Id
            select user;
    }
}