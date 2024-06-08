namespace Core.Application.Queries.Base;

/// <summary>
/// Pagination information
/// </summary>
public sealed class PaginationInfo
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int Count { get; }
    public int TotalItems { get; }
    public bool HasPreviousPage => TotalItems > 0 && PageIndex > 1;
    public bool HasNextPage => PageSize * PageIndex < TotalItems;

    private PaginationInfo()
    {
    }

    private PaginationInfo(int pageIndex, int pageSize, int count, int totalItems)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalItems = totalItems;
        Count = count;
    }

    public static PaginationInfo Create(int pageIndex, int pageSize, int count, int totalItems)
        => new(pageIndex, pageSize, count, totalItems);
}