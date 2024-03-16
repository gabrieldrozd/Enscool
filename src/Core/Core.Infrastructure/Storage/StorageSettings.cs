using Common.Utilities.Abstractions;

namespace Core.Infrastructure.Storage;

public class StorageSettings : ISettings
{
    public const string SectionName = "StorageSettings";

    public string ConnectionString { get; set; } = string.Empty;
}