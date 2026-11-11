using PracticeProject.Domain.BaseInfo.Person;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Domain.BaseInfo.User
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        
        public Person.Person Person { get; set; }

        public Role Role { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }

        public User(Role role, string email, string password, Person.Person person)
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
