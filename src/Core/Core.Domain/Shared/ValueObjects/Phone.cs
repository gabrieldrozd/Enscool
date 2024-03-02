using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Core.Domain.Primitives;

namespace Core.Domain.Shared.ValueObjects;

public sealed class Phone : ValueObject
{
    private const string PhoneRegexPattern = @"^(?:\+?48)?(?:[ -]?[0-9]){9}$";

    public string Value { get; }

    public Phone(string value)
    {
        value = value.Contains("+48") ? value : $"+48{value}";

        Ensure.Not.NullOrEmpty(value);
        Ensure.Not.InvalidFormat(value, PhoneRegexPattern);

        Value = value;
    }

    public static Phone Parse(string value) => new(value);

    public static bool TryParse(string value, out Phone phone)
    {
        try
        {
            phone = Parse(value);
            return true;
        }
        catch
        {
            phone = default!;
            return false;
        }
    }

    public static implicit operator string(Phone phone) => phone.Value;
    public static implicit operator Phone(string phone) => new(phone);

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}