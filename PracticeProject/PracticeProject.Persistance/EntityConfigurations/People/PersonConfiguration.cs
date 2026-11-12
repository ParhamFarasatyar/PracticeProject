using PracticeProject.Domain.BaseInfo.People;
using System.Data.Entity.ModelConfiguration;

namespace PracticeProject.Persistance.EntityConfigurations.People
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.FirstName).IsRequired().HasMaxLength(100);

            Property(p => p.LastName).IsOptional().HasMaxLength(100);

            Property(p => p.Age).IsRequired();
        }
    }
}
