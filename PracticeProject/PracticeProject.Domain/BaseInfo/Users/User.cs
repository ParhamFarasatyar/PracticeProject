using PracticeProject.Domain.BaseInfo.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Domain.BaseInfo.Users
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        
        public Person Person { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public User(Role role, string email, string password, Person person)
        {
            Role = role;
            Email = email;
            Password = password;
            Person = person ?? throw new System.ArgumentNullException(nameof(person));
        }

        public User()
        {
            
        }
    }
}
