using System.Data.Entity;
using System.Linq;
using PracticeProject.Application;
using PracticeProject.Domain.BaseInfo.People;
using PracticeProject.Domain.BaseInfo.Users;
using PracticeProject.Persistance.EntityConfigurations.People;
using PracticeProject.Persistance.EntityConfigurations.Users;

namespace PracticeProject.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDb
    {
        public DataBaseContext() : base("name=PracticeProject")
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Person> People { get; set; }

        IQueryable<TEntity> IDb.Query<TEntity>() => base.Set<TEntity>().AsNoTracking();

        IQueryable<TEntity> IDb.Set<TEntity>() => base.Set<TEntity>();

        TEntity IDb.Add<TEntity>(TEntity entity) => base.Set<TEntity>().Add(entity);

        void IDb.Remove<TEntity>(TEntity entity) => base.Set<TEntity>().Remove(entity);

        void IDb.DeleteById<TEntity>(int id)
        {
            var entity = new TEntity
            {
                Id = id
            };

            base.Set<TEntity>().Attach(entity);
            base.Set<TEntity>().Remove(entity);
        }

        void IDb.Update<TEntity>(TEntity entity) => base.Entry(entity).State = EntityState.Modified;

        int IDb.SaveChanges() => base.SaveChanges();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
