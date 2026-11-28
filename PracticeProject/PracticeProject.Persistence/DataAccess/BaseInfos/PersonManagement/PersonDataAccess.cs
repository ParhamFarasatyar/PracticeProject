using System.Linq;
using PracticeProject.Application;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Domain.BaseInfos.People;

namespace PracticeProject.Persistence.DataAccess.BaseInfos.PersonManagement;

public class PersonDataAccess(IDb db, ISearchPersons searchPersons) : IPersonDataAccess
{
    public void Delete(int id)
    {
        var person = GetById(id);

        if (person is null)
            return;

        db.Remove(person);
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
        db.Update(person);
    }
}