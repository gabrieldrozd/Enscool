namespace Core.Application.Queries.Browse.Search;

public sealed record SearchModel(string? SearchValue = null, SearchPattern? SearchPattern = null, string[]? PropertyNames = null);