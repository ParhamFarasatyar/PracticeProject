using System.Linq;
using PracticeProject.Domain;

namespace PracticeProject.Application.BaseInfos.Extensions;

public static class SearchExtensions
{
    public static IQueryable<TEntity> FilterById<TEntity>(this IQueryable<TEntity> items, int? id) where TEntity : IEntity
    {
        return items.Where(r => r.Id == id);
    }
}