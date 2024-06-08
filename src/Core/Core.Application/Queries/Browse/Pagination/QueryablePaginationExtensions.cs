namespace Core.Application.Queries.Browse.Pagination;

public static class QueryablePaginationExtensions
{
    public static IQueryable<TEntity> WithPagination<TEntity>(this IQueryable<TEntity> queryable, PaginationModel pagination)
    {
        var pageSize = pagination.PageSize != 0 ? pagination.PageSize : 10;
        var skipValue = pagination.PageIndex != 0 ? (pagination.PageIndex - 1) * pageSize : 0;

        return queryable
            .Skip(skipValue)
            .Take(pageSize);
    }
}