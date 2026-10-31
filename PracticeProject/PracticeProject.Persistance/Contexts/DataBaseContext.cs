using System.Data.Entity;
using PracticeProject.Domain.Entities;

namespace PracticeProject.Persistence.Contexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=PracticeProject")
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
