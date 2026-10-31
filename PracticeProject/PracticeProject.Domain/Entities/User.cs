using PracticeProject.Domain.Enums;

namespace PracticeProject.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
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
    }
}
