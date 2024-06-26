namespace Core.Application.Queries.Browse;

public sealed record BrowseModel
{
    public int Page { get; set; }
    public int Size { get; set; }

    public string? Sort { get; set; }
    public SortOrder? Order { get; set; }

    public string? Search { get; set; }

    public static BrowseModel Default => new()
    {
        Page = 1,
        Size = 10
    };
}