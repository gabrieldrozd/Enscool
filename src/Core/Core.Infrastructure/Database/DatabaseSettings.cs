namespace Core.Infrastructure.Database;

internal sealed class DatabaseSettings
{
    public const string SectionName = "Database";

    public string ConnectionString { get; set; } = default!;
}