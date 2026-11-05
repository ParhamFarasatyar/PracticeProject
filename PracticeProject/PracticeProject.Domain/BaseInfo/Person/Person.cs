namespace PracticeProject.Domain.BaseInfo.Person
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
    }
}
