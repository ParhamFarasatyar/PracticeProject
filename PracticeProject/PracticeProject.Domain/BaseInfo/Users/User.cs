using System;
using PracticeProject.Domain.BaseInfos.People;

namespace PracticeProject.Domain.BaseInfos.Users
{
    public class User : IEntity
    {
        public User(Role role, string email, string password, Person person)
        {
            Role = role;
            Email = email;
            Password = password;
            Person = person ?? throw new ArgumentNullException(nameof(person));
        }

        public User()
        {
        }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public int Id { get; set; }
    }
}