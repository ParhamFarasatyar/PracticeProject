using PracticeProject.Domain.BaseInfos.People;

namespace PracticeProject.Application.BaseInfos
{
    public interface IPersonDataAccess
    {
        public void Delete(int id);

        public void Update(Person person);

        public Person GetById(int id);
    }
}
