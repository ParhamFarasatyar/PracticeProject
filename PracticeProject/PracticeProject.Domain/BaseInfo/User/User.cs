using PracticeProject.Domain.BaseInfo.Person;
using System.ComponentModel.DataAnnotations;

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

        public User(
            int id, Role role,
            string email, string password
            )
        {
            Id = id;
            Role = role;
            Email = email;
            Password = password;
        }
        public User()
        {
            
        }
    }
}
