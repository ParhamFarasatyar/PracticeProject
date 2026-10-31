using PracticeProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }

        public User(
            int id, Role role,
            string firstName, string lastName,
            string email, string password
            )
        {
            Id = id;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }
        public User()
        {
            
        }
    }
}
