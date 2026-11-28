namespace PracticeProject.Domain.BaseInfos.People
{
    public class Person : IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
        public int Id { get; set; }
    }
}