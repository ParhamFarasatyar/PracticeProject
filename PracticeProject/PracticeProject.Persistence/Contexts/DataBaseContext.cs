using System.Data.Entity;
using System.Linq;
using PracticeProject.Application;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using PracticeProject.Persistence.EntityConfigurations.People;
using PracticeProject.Persistence.EntityConfigurations.Users;

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

        void IDb.Remove<TEntity>(TEntity entity)
        {
            var entry = base.Entry(entity);

            if (entry.State == EntityState.Detached)
                base.Set<TEntity>().Attach(entity);

            base.Set<TEntity>().Remove(entity);
        }

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
