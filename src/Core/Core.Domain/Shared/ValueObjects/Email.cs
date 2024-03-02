using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Core.Domain.Primitives;

namespace Core.Domain.Shared.ValueObjects;

public sealed class Email : ValueObject
{
    private const string EmailRegexPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

    public string Value { get; }

    public Email(string email)
    {
        Ensure.Not.NullOrEmpty(email);
        Ensure.Not.InvalidFormat(email, EmailRegexPattern);

        Value = email.ToLowerInvariant();
    }

    public static Email Parse(string value) => new(value);

    public static bool TryParse(string value, out Email email)
    {
        try
        {
            email = Parse(value);
            return true;
        }
        catch
        {
            email = default!;
            return false;
        }
    }

    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new(email);

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}