namespace Core.Application.Queries.Browse.Pagination;

/// <summary>
/// Pagination object
/// </summary>
public sealed record PaginationModel(int PageIndex = 1, int PageSize = 10)
{
    public static PaginationModel Default => new();
}