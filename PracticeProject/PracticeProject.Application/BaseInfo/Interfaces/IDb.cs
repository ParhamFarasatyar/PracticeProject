using PracticeProject.Domain;
using System;
using System.Linq;

namespace PracticeProject.Application.BaseInfo.Interfaces
{
    public interface IDb : IDisposable
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;

        IQueryable<TEntity> Set<TEntity>() where TEntity : class, IEntity;

        TEntity Add<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void DeleteById<TEntity>(int id) where TEntity: class, IEntity, new();

        int SaveChanges();
    }
}
