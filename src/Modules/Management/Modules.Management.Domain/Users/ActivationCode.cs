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
        var code = GenerateRandomAlphanumericString(32);
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

    private static string GenerateRandomAlphanumericString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var data = new byte[length];
        var result = new char[length];

        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(data);

        for (var i = 0; i < length; i++)
            result[i] = chars[data[i] % chars.Length];

        return new string(result);
    }
}