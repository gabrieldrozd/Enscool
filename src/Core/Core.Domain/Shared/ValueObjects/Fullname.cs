using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Common.Utilities.Resources;
using Core.Domain.Primitives;

namespace Core.Domain.Shared.ValueObjects;

public sealed class Fullname : ValueObject
{
    private const string Separator = ";";

    public string First { get; }
    public string? Middle { get; }
    public string Last { get; }

    private Fullname(string first, string? middle, string last)
    {
        Ensure.Not.NullOrEmpty(first);
        Ensure.Not.NullOrEmpty(last);

        First = first;
        Middle = middle;
        Last = last;
    }

    public static Fullname Create(string first, string? middle, string last)
        => new(first, middle, last);

    public static Fullname FromString(string fullName)
    {
        var names = fullName.Split(Separator);
        return names.Length switch
        {
            2 => new Fullname(names[0], null, names[1]),
            3 => new Fullname(names[0], names[1], names[2]),
            _ => throw new DomainException(Resource.InvalidFullName)
        };
    }

    public static implicit operator string(Fullname fullname)
        => string.IsNullOrEmpty(fullname.Middle)
            ? $"{fullname.First}{Separator}{fullname.Last}"
            : $"{fullname.First}{Separator}{fullname.Middle}{Separator}{fullname.Last}";

    public static implicit operator Fullname((string, string?, string) fullName)
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