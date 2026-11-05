using System.Data.Entity;
using System.Linq;
using PracticeProject.Application.BaseInfo.Interfaces;
using PracticeProject.Domain.BaseInfo.User;

namespace PracticeProject.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDb
    {
        public DataBaseContext() : base("name=PracticeProject")
        {
            
        }

        public DbSet<User> Users { get; set; }

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
    }
}
