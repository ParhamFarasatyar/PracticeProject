using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using PracticeProject.Domain.BaseInfo.User;

namespace PracticeProject.Persistance.EntityConfigurations.Users
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(u => u.Id);

            HasRequired(u => u.Person).WithMany().HasForeignKey(u => u.PersonId);

            Property(u => u.PersonId).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAttribute("IX_User_PersonId")
                {
                    IsUnique = true
                });

            Property(u => u.Email).IsRequired().HasMaxLength(250);

            Property(u => u.Password).IsRequired().HasMaxLength(150);
        }
    }
}
