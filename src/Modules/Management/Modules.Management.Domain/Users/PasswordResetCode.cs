using Core.Domain.Primitives;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users;

public sealed class PasswordResetCode : ValueObject
{
    public string Value { get; } = string.Empty;
    public Date Expires { get; } = default!;
    public bool IsActive { get; private set; } = true;
    public Date CreatedAt { get; } = Date.UtcNow;

    private PasswordResetCode()
    {
    }

    private PasswordResetCode(string value, Date expires, bool isActive = true)
    {
        Value = value;
        Expires = expires;
        IsActive = isActive;
    }

    public static PasswordResetCode Create(int expiryInHours)
    {
        var firstSection = Random.Shared.Next(1000, 9999);
        var secondSection = Random.Shared.Next(1000, 9999);
        var code = $"{firstSection}-{secondSection}";
        return new PasswordResetCode(code, Date.UtcNow.AddHours(expiryInHours));
    }

    public bool Verify(string inputValue)
    {
        if (!IsActive) return false;
        if (Date.UtcNow > Expires) return false;
        return Value == inputValue;
    }

    public void Deactivate() => IsActive = false;

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return Expires;
        yield return IsActive;
    }
}