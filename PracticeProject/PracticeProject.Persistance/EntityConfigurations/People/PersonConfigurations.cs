using PracticeProject.Domain.BaseInfo.Person;
using System.Data.Entity.ModelConfiguration;

namespace PracticeProject.Persistance.EntityConfigurations.People
{
    public class PersonConfigurations : EntityTypeConfiguration<Person>
    {
        public PersonConfigurations()
        {
            HasKey(p => p.Id);

            Property(p => p.FirstName).IsRequired().HasMaxLength(100);

            Property(p => p.LastName).IsOptional().HasMaxLength(100);

            Property(p => p.Age).IsRequired();
        }
    }
}
