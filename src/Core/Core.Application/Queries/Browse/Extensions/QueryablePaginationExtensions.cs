namespace Core.Application.Queries.Browse.Extensions;

public static class QueryablePaginationExtensions
{
    public static IQueryable<TEntity> WithPagination<TEntity>(this IQueryable<TEntity> queryable, int pageIndex = 1, int pageSize = 10)
    {
        var size = pageSize != 0 ? pageSize : 10;
        var skipValue = pageIndex != 0 ? (pageIndex - 1) * size : 0;

        return queryable
            .Skip(skipValue)
            .Take(size);
    }
}