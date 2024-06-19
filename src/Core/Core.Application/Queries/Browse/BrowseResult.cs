using Common.Utilities.Abstractions.Mapping;
using Core.Application.Queries.Base;

namespace Core.Application.Queries.Browse;

public sealed record BrowseResult<TEntity> where TEntity : class
{
    public PaginationInfo Pagination { get; }
    public IReadOnlyList<TEntity> List { get; }

    private BrowseResult(PaginationInfo pagination, IReadOnlyList<TEntity> list)
    {
        Pagination = pagination;
        List = list;
    }

    public static BrowseResult<TEntity> Create(int pageIndex, int pageSize, int totalItems, List<TEntity> items)
    {
        var paginationInfo = PaginationInfo.Create(pageIndex, pageSize, items.Count, totalItems);
        return new BrowseResult<TEntity>(paginationInfo, items);
    }

    public BrowseResult<TDto> MapTo<TDto>()
        where TDto : class, IWithExpressionMapFrom<TEntity, TDto>
    {
        var list = List.Select(TDto.GetMapping().Compile());
        return new BrowseResult<TDto>(Pagination, [..list]);
    }
}