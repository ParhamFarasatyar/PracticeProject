using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Domain.BaseInfo.Person
{
    public class Person : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public int? Age { get; set; }
        public Gender Gender { get; set; }
    }
}
