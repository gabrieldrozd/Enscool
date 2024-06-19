using System.Reflection;

namespace Common.Utilities.Extensions;

public static class TypeExtensions
{
    public static PropertyInfo? GetTypeProperty(this Type? type, string name)
    {
        while (true)
        {
            if (type != null && type == typeof(object))
                return null;

            ArgumentNullException.ThrowIfNull(type);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            var typeProperty = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (typeProperty != null)
                return typeProperty;

            type = type.BaseType;
        }
    }

    public static bool InheritsOrImplements(this Type? child, Type? parent)
    {
        if (child is null || parent is null)
            return false;

        if (parent.IsInterface)
            return child.GetInterface(parent.Name) != null;

        while (child != null && child != typeof(object))
        {
            var cur = child.IsGenericType ? child.GetGenericTypeDefinition() : child;
            if (parent == cur)
                return true;

            child = child.BaseType;
        }

        return false;
    }
}