using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users;

/// <summary>
/// Represents user in the system.
/// </summary>
public sealed class User : AggregateRoot<UserId>
{
    public UserState State { get; private set; } = UserState.Pending;
    public Email Email { get; private set; } = default!;
    public Phone Phone { get; private set; } = default!;
    public Password? Password { get; private set; }
    public FullName FullName { get; private set; } = default!;
    public UserRole Role { get; }

    private User()
    {
    }

    private User(
        UserId id,
        Email email,
        Phone phone,
        FullName fullName,
        UserRole role,
        InstitutionId? institutionId
    )
        : base(id)
    {
        SetInstitutionId(institutionId);

        Email = email;
        Phone = phone;
        FullName = fullName;
        Role = role;
    }

    /// <summary>
    /// Creates new <see cref="User"/>.
    /// </summary>
    public static User Create(UserId id, Email email, Phone phone, FullName fullName, UserRole role, InstitutionId? institutionId)
        => new(id, email, phone, fullName, role, institutionId);
}