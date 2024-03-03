namespace Core.Infrastructure.Abstractions.Modules.Swagger.Settings;

public static class ApiGroups
{
    public const string Management = "management-module";

    public static IDictionary<string, string> GetNameValueDictionary()
    {
        var nameValue = new Dictionary<string, string>();

        object? structValue = default(ApiGroups);

        foreach (var group in typeof(ApiGroups).GetFields())
        {
            var value = group.GetValue(structValue)?.ToString();
            var name = group.Name;
            if (value is not null)
            {
                nameValue.Add(name, value);
            }
        }

        return nameValue;
    }
}