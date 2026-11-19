using PracticeProject.Domain.BaseInfos.People;
using System.Linq;

namespace PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons
{
    public interface ISearchPersons
    {
       IQueryable<Person> Execute(Request requset = null);

        class Request
        {
            public int? Id { get; set; }
            public string Keyword { get; set; }
        }
    }
}
