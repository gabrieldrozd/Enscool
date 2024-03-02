using System.Reflection;

namespace Core.Domain.Primitives.Enumerations;

public abstract record SimpleEnumeration<TEnum>(string Value)
    where TEnum : SimpleEnumeration<TEnum>
{
    private static readonly IReadOnlyDictionary<int, TEnum> Enumerations = CreateEnumerations();
    public static readonly IReadOnlyList<TEnum> Values = Enumerations.Values.ToList();

    protected static TEnum? From(string value)
        => Enumerations.Values.SingleOrDefault(x => x.Value == value);

    public virtual bool Equals(SimpleEnumeration<TEnum>? other)
        => other != null && GetType() == other.GetType() && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);
        var enumerations = new Dictionary<int, TEnum>();
        var index = 0;

        AddEnumerationsFromType(enumerationType, enumerationType, enumerations, ref index);

        var nestedTypes = enumerationType.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);
        foreach (var nestedType in nestedTypes)
        {
            AddEnumerationsFromType(nestedType, enumerationType, enumerations, ref index);
        }

        return enumerations;
    }

    private static void AddEnumerationsFromType(Type type, Type enumerationType, Dictionary<int, TEnum>? enumerations, ref int index)
    {
        var fieldsForType = type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum) fieldInfo.GetValue(default)!);

        foreach (var field in fieldsForType)
        {
            enumerations?.Add(index++, field);
        }
    }
}