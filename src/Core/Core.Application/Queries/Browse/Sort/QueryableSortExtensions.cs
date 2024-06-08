using System.Linq.Expressions;
using Common.Utilities.Extensions;
using Core.Domain.Primitives;

namespace Core.Application.Queries.Browse.Sort;

public static class QueryableSortExtensions
{
    public static IQueryable<TEntity> WithDynamicSort<TEntity>(this IQueryable<TEntity> query, SortModel? sortModel)
        where TEntity : class, IEntity
    {
        var sortBy = sortModel?.SortBy ?? nameof(IEntity.CreatedOnUtc);
        var propertyInfo = typeof(TEntity).GetTypeProperty(sortBy);
        if (propertyInfo is null)
            return query;

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda(property, parameter);

        var orderByMethod = sortModel?.SortOrder switch
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