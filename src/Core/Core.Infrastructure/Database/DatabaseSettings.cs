namespace Core.Infrastructure.Database;

internal sealed class DatabaseSettings
{
    public const string SectionName = "DatabaseSettings";

    public string ConnectionString { get; set; } = default!;
}