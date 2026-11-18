using PracticeProject.Application;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPeople;
using PracticeProject.Domain.BaseInfos.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeProject.Persistance.DataAccess.BaseInfos
{
    public class SearchPersons : ISearchPeople
    {
        private readonly IDb _db;

        public SearchPersons(IDb db)
        {
            _db = db;
        }
        public IQueryable<Person> Execute(ISearchPeople.Request request = null)
        {
            var items = _db.Query<Person>();

            if (request == null)
                return items;

            if (request.Id.HasValue)
                items = items.Where(i => i.Id == request.Id);

            if (!string.IsNullOrEmpty(request.Keyword))
                items = items.Where(i => 
                i.FirstName.Contains(request.Keyword) || 
                i.LastName.Contains(request.Keyword));

            return items;
        }
    }
}
