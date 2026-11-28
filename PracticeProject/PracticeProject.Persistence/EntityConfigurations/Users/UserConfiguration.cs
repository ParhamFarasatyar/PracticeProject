using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Persistence.EntityConfigurations.Users;

public class UserConfiguration : EntityTypeConfiguration<User>
{
    public UserConfiguration()
    {
        HasKey(u => u.Id);

        HasRequired(u => u.Person).WithMany().HasForeignKey(u => u.PersonId).WillCascadeOnDelete(true);

        Property(u => u.PersonId).HasColumnAnnotation(
            IndexAnnotation.AnnotationName,
            new IndexAnnotation(
                new IndexAttribute("IX_User_PersonId")
                {
                    IsUnique = true
                }
            )
        );

        Property(u => u.Email).IsRequired().HasMaxLength(250);

        Property(u => u.Password).IsRequired().HasMaxLength(500);
    }
}