using System.Linq;
using PracticeProject.Domain.BaseInfos.People;

namespace PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;

public interface ISearchPersons
{
    IQueryable<Person> Respond(Request requset = null);

    class Request
    {
        public int? Id { get; set; }
        public string Keyword { get; set; }
    }
}