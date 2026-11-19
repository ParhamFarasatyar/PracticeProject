using PracticeProject.Application;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Domain.BaseInfos.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeProject.Persistance.DataAccess.BaseInfos.Security.BaseInfos.PersonManagement
{
    public class SearchPersons : ISearchPersons
    {
        private readonly IDb _db;

        public SearchPersons(IDb db)
        {
            _db = db;
        }
        public IQueryable<Person> Execute(ISearchPersons.Request request = null)
        {
            var items = _db.Query<Person>();

            return ValidateRequest(request, ref items);
        }

        private IQueryable<Person> ValidateRequest(ISearchPersons.Request request, ref IQueryable<Person> items)
        {
            if(request == null)
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
