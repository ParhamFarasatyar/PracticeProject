using System.Linq;
using PracticeProject.Domain.BaseInfos.People;

namespace PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;

public static class SearchPersonsExtensions
{
    public static IQueryable<Person> FilterByName(this IQueryable<Person> items, string keyword)
    {
        return items.Where(i => 
            i.FirstName.Contains(keyword) || i.LastName.Contains(keyword));
    }
}