using Common.Utilities.Abstractions;

namespace Core.Infrastructure.Database;

internal sealed class DatabaseSettings : ISettings
{
    public const string SectionName = "DatabaseSettings";

    public string ConnectionString { get; set; } = default!;
}