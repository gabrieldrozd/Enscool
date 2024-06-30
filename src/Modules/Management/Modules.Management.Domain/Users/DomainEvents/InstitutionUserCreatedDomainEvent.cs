using Core.Domain.Communication.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.DomainEvents;

/// <summary>
/// Raised when an institution user has been created
/// </summary>
public sealed record InstitutionUserCreatedDomainEvent : IDomainEvent
{
    public UserId UserId { get; init; }
    public UserState State { get; init; }
    public Email Email { get; init; }
    public Phone Phone { get; init; }
    public string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public string LastName { get; init; }
    public UserRole Role { get; init; }
    public Date? BirthDate { get; init; }
    public Address? Address { get; init; }
    public LanguageLevel? LanguageLevel { get; init; }
    public InstitutionId InstitutionId { get; init; }

    /// <summary>
    /// Raised when an institution user has been created
    /// </summary>
    public InstitutionUserCreatedDomainEvent(InstitutionUser institutionUser)
    {
        UserId = institutionUser.Id;
        State = institutionUser.State;
        Email = institutionUser.Email;
        Phone = institutionUser.Phone;
        FirstName = institutionUser.FirstName;
        MiddleName = institutionUser.MiddleName;
        LastName = institutionUser.LastName;
        Role = institutionUser.Role;
        BirthDate = institutionUser.BirthDate;
        Address = institutionUser.Address;
        LanguageLevel = institutionUser.LanguageLevel;
        InstitutionId = institutionUser.InstitutionId!;
    }
}