using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;

namespace Core.Application.Auth;

/// <summary>
/// Represents a <see cref="RefreshToken"/>.
/// </summary>
public sealed class RefreshToken
{
    public string Value { get; }
    public Guid UserId { get; }
    public DateTime Created { get; }
    public DateTime Expires { get; }
    public bool IsExpired => Date.UtcNow >= Expires;

    private RefreshToken(UserId userId, string value, int expiryInHours)
    {
        UserId = userId;
        Value = value;

        Created = Date.UtcNow;
        Expires = Created.AddHours(expiryInHours);
    }

    /// <summary>
    /// Creates new instance of <see cref="RefreshToken"/>.
    /// </summary>
    public static RefreshToken Create(UserId userId, string value, int expiryInHours)
        => new(userId, value, expiryInHours);
}