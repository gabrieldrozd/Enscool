using System.Linq.Expressions;
using Common.Utilities.Extensions;
using Core.Domain.Primitives;

namespace Core.Application.Queries.Browse.Extensions;

public static class QueryableSortExtensions
{
    public static IQueryable<TEntity> WithDynamicSort<TEntity>(this IQueryable<TEntity> query, string? sortBy, SortOrder? sortOrder)
        where TEntity : class, IEntity
    {
        sortBy ??= nameof(IEntity.CreatedOnUtc);
        var propertyInfo = typeof(TEntity).GetTypeProperty(sortBy);
        if (propertyInfo is null)
            return query;

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda(property, parameter);

        var orderByMethod = sortOrder switch
        {
            SortOrder.Ascending => "OrderBy",
            SortOrder.Descending => "OrderByDescending",
            _ => null
        };

        if (orderByMethod is null)
            return query;

        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == orderByMethod && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(TEntity), propertyInfo.PropertyType);

        return (IQueryable<TEntity>) method.Invoke(null, [query, lambda])!;
    }
}