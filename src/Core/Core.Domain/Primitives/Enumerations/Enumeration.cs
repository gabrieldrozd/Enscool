using System.Reflection;

namespace Core.Domain.Primitives.Enumerations;

public abstract record Enumeration<TEnum>(int Id, string Value)
    where TEnum : Enumeration<TEnum>
{
    protected static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();
    public static readonly IReadOnlyList<TEnum> All = Enumerations.Values.ToList();

    public static bool IsValid(int id)
        => Enumerations.ContainsKey(id);

    public static bool IsValid(string value)
        => Enumerations.Values.Any(x => x.Value == value);

    protected static TEnum? FromId(int id)
        => Enumerations.TryGetValue(id, out var enumeration)
            ? enumeration
            : default;

    protected static TEnum? FromValue(string value)
        => Enumerations.Values.SingleOrDefault(x => x.Value == value);

    public virtual bool Equals(Enumeration<TEnum>? other)
        => other switch
        {
            null => false,
            not null => GetType() == other.GetType() && Id == other.Id
        };

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => Value;

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo =>
                (TEnum) fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Id);
    }
}