using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Common.Utilities.Resources;
using Core.Domain.Primitives;

namespace Core.Domain.Shared.ValueObjects;

public sealed class FullName : ValueObject
{
    private const string Separator = ";";

    public string First { get; }
    public string? Middle { get; }
    public string Last { get; }

    private FullName(string first, string? middle, string last)
    {
        Ensure.Not.NullOrEmpty(first);
        Ensure.Not.NullOrEmpty(last);

        First = first;
        Middle = middle;
        Last = last;
    }

    public static FullName Create(string first, string? middle, string last)
        => new(first, middle, last);

    public static FullName FromString(string fullName)
    {
        var names = fullName.Split(Separator);
        return names.Length switch
        {
            2 => new FullName(names[0], null, names[1]),
            3 => new FullName(names[0], names[1], names[2]),
            _ => throw new DomainException(Resource.InvalidFullName)
        };
    }

    public static implicit operator string(FullName fullName)
        => string.IsNullOrEmpty(fullName.Middle)
            ? $"{fullName.First}{Separator}{fullName.Last}"
            : $"{fullName.First}{Separator}{fullName.Middle}{Separator}{fullName.Last}";

    public static implicit operator FullName((string, string?, string) fullName)
        => new(fullName.Item1, fullName.Item2, fullName.Item3);

    public override string ToString()
        => string.IsNullOrEmpty(Middle)
            ? $"{First}{Separator}{Last}"
            : $"{First}{Separator}{Middle}{Separator}{Last}";

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return First;
        if (Middle != null)
            yield return Middle;
        yield return Last;
    }
}