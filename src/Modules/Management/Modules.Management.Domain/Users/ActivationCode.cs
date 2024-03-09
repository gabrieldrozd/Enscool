using System.Security.Cryptography;
using Core.Domain.Primitives;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users;

public sealed class ActivationCode : ValueObject
{
    public string Value { get; } = string.Empty;
    public Date Expires { get; } = default!;
    public bool IsActive { get; private set; } = true;
    public Date CreatedAt { get; } = Date.UtcNow;

    private ActivationCode()
    {
    }

    private ActivationCode(string value, Date expires, bool isActive = true)
    {
        Value = value;
        Expires = expires;
        IsActive = isActive;
    }

    public static ActivationCode Create(int expiryInHours)
    {
        var hmac = new HMACSHA256();
        var code = Convert.ToBase64String(hmac.Key);
        return new ActivationCode(code, Date.UtcNow.AddHours(expiryInHours));
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