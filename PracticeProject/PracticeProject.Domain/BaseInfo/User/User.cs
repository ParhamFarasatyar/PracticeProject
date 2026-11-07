using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Domain.BaseInfo.User
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [Index("IX_User_PersonId", IsUnique = true)]
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
