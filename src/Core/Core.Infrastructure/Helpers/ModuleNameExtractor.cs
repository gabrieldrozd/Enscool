namespace Core.Infrastructure.Helpers;

public static class ModuleNameExtractor
{
    /// <summary>
    /// Extracts the module name from the namespace of the given type.
    /// </summary>
    /// <param name="value">The value to extract the module name from.</param>
    /// <returns>The string representation of the module name.</returns>
    public static string GetModuleName(this object? value)
        => value?.GetType().GetModuleName() ?? string.Empty;

    private static string GetModuleName(this Type? type)
    {
        const string modulePart = "Modules.";
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        var moduleName = type.Namespace.StartsWith(modulePart)
            ? type.Namespace.Split(".")[2].ToLowerInvariant()
            : string.Empty;

        return moduleName;
    }
}