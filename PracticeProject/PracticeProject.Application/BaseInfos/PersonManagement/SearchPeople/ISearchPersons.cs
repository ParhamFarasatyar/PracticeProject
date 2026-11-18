using PracticeProject.Domain.BaseInfos.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Application.BaseInfos.PersonManagement.SearchPeople
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
