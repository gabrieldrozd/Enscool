using System.Diagnostics.CodeAnalysis;
using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Domain.Users.DomainEvents;

namespace Modules.Management.Domain.Users;

/// <summary>
/// Represents user in the system.
/// </summary>
public class User : AggregateRoot<UserId>
{
    private readonly List<ActivationCode> _activationCodes = [];
    private readonly List<PasswordResetCode> _passwordResetCodes = [];
    private readonly List<InstitutionId> _institutionIds = [];

    public UserState State { get; private set; } = UserState.Pending;
    public Email Email { get; private set; } = default!;
    public Phone Phone { get; protected set; } = default!;
    public Password? Password { get; private set; }
    public string FirstName { get; protected set; } = default!;
    public string? MiddleName { get; protected set; }
    public string LastName { get; protected set; } = default!;
    public UserRole Role { get; }

    public IReadOnlyList<ActivationCode> ActivationCodes => _activationCodes.AsReadOnly();
    public IReadOnlyList<PasswordResetCode> PasswordResetCodes => _passwordResetCodes.AsReadOnly();
    public IReadOnlyList<InstitutionId> InstitutionIds => _institutionIds.AsReadOnly();

    public ActivationCode? CurrentActivationCode => _activationCodes.MaxBy(x => x.CreatedAt);
    public PasswordResetCode? CurrentPasswordResetCode => _passwordResetCodes.MaxBy(x => x.CreatedAt);

    protected User()
    {
    }

    protected User(
        UserId id,
        Email email,
        Phone phone,
        string firstName,
        string? middleName,
        string lastName,
        UserRole role,
        InstitutionId? institutionId,
        List<InstitutionId> institutionIds
    )
        : base(id)
    {
        SetInstitutionId(institutionId);

        Email = email;
        Phone = phone;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Role = role;

        _institutionIds.AddRange(institutionIds);
    }

    #region Activation

    /// <summary>
    /// Adds activation code to the <see cref="User"/>.
    /// Deactivates all previous activation codes.
    /// </summary>
    protected void AddActivationCode(ActivationCode code)
    {
        _activationCodes.ForEach(x => x.Deactivate());
        _activationCodes.Add(code);
    }

    public void ActivateAccount(Password password)
    {
        _activationCodes.ForEach(x => x.Deactivate());
        State = UserState.Active;
        Password = password;
    }

    #endregion

    #region Password

    public void ChangePassword(Password password)
        => Password = password;

    public void AddPasswordResetCode(PasswordResetCode code)
    {
        _passwordResetCodes.ForEach(x => x.Deactivate());
        _passwordResetCodes.Add(code);
    }

    public void ResetPassword(Password create)
    {
        _passwordResetCodes.ForEach(x => x.Deactivate());
        Password = create;
    }

    #endregion

    #region State

    // TODO NOTE: Option to activate, after user was Deactivated
    public void Activate()
    {
        State.ValidateTransitionTo(UserState.Active);
        State = UserState.Active;

        // TODO: Raise domain event
    }

    public void Deactivate()
    {
        State.ValidateTransitionTo(UserState.Inactive);
        State = UserState.Inactive;

        RaiseDomainEvent(new UserDeactivatedDomainEvent(Id, Role));
    }

    public override void Restore()
    {
        State.ValidateTransitionTo(UserState.Active);
        State = UserState.Active;
        base.Restore();

        RaiseDomainEvent(new UserRestoredDomainEvent(Id, Role));
    }

    public override void Delete()
    {
        State.ValidateTransitionTo(UserState.Deleted);
        State = UserState.Deleted;
        base.Delete();

        RaiseDomainEvent(new UserDeletedDomainEvent(Id, Role));
    }

    #endregion

    [MemberNotNullWhen(true, nameof(CurrentPasswordResetCode))]
    public bool CanResetPassword()
        => State is UserState.Active && CurrentPasswordResetCode is not null;

    [MemberNotNullWhen(true, nameof(CurrentActivationCode))]
    public bool CanBeActivated()
        => State is UserState.Pending && CurrentActivationCode is not null;

    public bool CanBeLoggedIn()
        => State is UserState.Active;
}