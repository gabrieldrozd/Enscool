using System.Linq.Expressions;
using Core.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Common.Utilities.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Extension method to add top-layer includes to a query.
    /// <code>
    /// _context.TableName.AddIncludes(x => x.TableToInclude)
    /// </code>
    /// </summary>
    public static IQueryable<TEntity> AddIncludes<TEntity>(
        this IQueryable<TEntity> query,
        params Expression<Func<TEntity, object>>[]? includes)
        where TEntity : class
    {
        if (includes is not null && includes.Length != 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return query;
    }

    /// <summary>
    /// Extension method to add nested includes to a query.
    /// <code>
    /// _context.TableName.AddIncludes(q => q.Include(t => t.TableToInclude)
    ///     .ThenInclude(q => q.NestedTableToInclude1)
    ///     .ThenInclude(q => q.NestedTableToInclude2));
    /// </code>
    /// </summary>
    public static IQueryable<TEntity> AddIncludes<TEntity>(
        this IQueryable<TEntity> query,
        params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes)
        where TEntity : class
    {
        return includes is not null
            ? includes.Aggregate(query, (current, include) => include(current))
            : query;
    }

    /// <summary>
    /// Extension method to check condition and apply where clause to query if condition is met.
    /// <code>
    /// _context.TableName.WhereIf(
    ///     !string.IsNullOrWhiteSpace(request.Status),
    ///     e =&gt; e.Status == request.Status)
    /// </code>
    /// </summary>
    /// <param name="query">Query to apply where clause to.</param>
    /// <param name="condition">Condition to check before applying where clause.</param>
    /// <param name="predicate">Where clause to apply to query.</param>
    /// <returns>Query with where clause applied if condition is met. Otherwise, returns original query.</returns>
    public static IQueryable<TEntity> WhereIf<TEntity>(
        this IQueryable<TEntity> query,
        bool condition,
        Expression<Func<TEntity, bool>> predicate
    ) => condition ? query.Where(predicate) : query;

    /// <summary>
    /// Extension method to check async condition and apply where clause to query if condition is met.
    /// <code>
    /// _context.TableName.WhereIf(
    ///     await _context.TableName.AnyAsync(),
    ///     e =&gt; e.Status == request.Status)
    /// </code>
    /// </summary>
    /// <param name="query">Query to apply where clause to.</param>
    /// <param name="condition">Async condition to check before applying where clause.</param>
    /// <param name="predicate">Where clause to apply to query.</param>
    /// <returns>Query with where clause applied if condition is met. Otherwise, returns original query.</returns>
    public static async Task<IQueryable<TEntity>> WhereIf<TEntity>(
        this IQueryable<TEntity> query,
        Task<bool> condition,
        Expression<Func<TEntity, bool>> predicate
    ) => await condition ? query.Where(predicate) : query;

    /// <summary>
    /// Extension method to check whether entity's collection of ids contains any of the ids to match.
    /// </summary>
    /// <param name="query">Query to apply where clause to.</param>
    /// <param name="idsPropertySelector">Selector for the collection of ids property.</param>
    /// <param name="idsToMatch">Ids to match.</param>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TId">Type of id <see cref="EntityId"/>.</typeparam>
    /// <returns>Query with applied ids matching where clause.</returns>
    public static IQueryable<TEntity> WhereAnyIdMatches<TEntity, TId>(
        this IQueryable<TEntity> query,
        Expression<Func<TEntity, IEnumerable<TId>>> idsPropertySelector,
        IEnumerable<Guid> idsToMatch)
        where TEntity : class, IEntity
        where TId : EntityId
    {
        var ids = idsToMatch as Guid[] ?? idsToMatch.ToArray();
        if (ids.Length == 0)
            return query;

        var idsSet = ids.ToHashSet();
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var collectionPropertyAccess = Expression.Invoke(idsPropertySelector, parameter);
        var hashSetInstance = Expression.Constant(idsSet, typeof(HashSet<Guid>));
        var containsMethodInfo = typeof(HashSet<Guid>).GetMethod(nameof(HashSet<Guid>.Contains), [typeof(Guid)]);
        var innerParameter = Expression.Parameter(typeof(TId), "id");
        var propertyAccess = Expression.PropertyOrField(innerParameter, "Value");
        var containsCall = Expression.Call(hashSetInstance, containsMethodInfo!, propertyAccess);
        var innerLambda = Expression.Lambda<Func<TId, bool>>(containsCall, innerParameter);
        var anyCall = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Any),
            [typeof(TId)],
            collectionPropertyAccess,
            innerLambda
        );

        var lambda = Expression.Lambda<Func<TEntity, bool>>(anyCall, parameter);
        return query.Where(lambda);
    }
}