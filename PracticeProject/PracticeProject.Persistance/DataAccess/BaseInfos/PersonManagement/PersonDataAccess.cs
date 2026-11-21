using PracticeProject.Application;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Domain.BaseInfos.People;
using System.Linq;

namespace PracticeProject.Persistance.DataAccess.BaseInfos.PersonManagement
{
    public class PersonDataAccess(IDb db, ISearchPersons searchPersons) : IPersonDataAccess
    {
        public void Delete(int id)
        {
            Person person = GetById(id);

            if (person is null)
                return;

            db.Remove<Person>(person);
        }

        public Person GetById(int id)
        {
            return searchPersons.Respond(new ISearchPersons.Request
            {
                Id = id
            }).SingleOrDefault(p => p.Id == id);
        }

        public void Update(Person person)
        {
            db.Update<Person>(person);
        }
    }
}
