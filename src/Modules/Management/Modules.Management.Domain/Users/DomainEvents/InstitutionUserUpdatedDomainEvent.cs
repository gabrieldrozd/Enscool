using Core.Domain.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.DomainEvents;

/// <summary>
/// Raised when an institution user has been updated
/// </summary>
public class InstitutionUserUpdatedDomainEvent : IDomainEvent
{
    public UserId UserId { get; init; }
    public Phone Phone { get; init; }
    public string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public string LastName { get; init; }
    public UserRole Role { get; init; }
    public Address? Address { get; init; }
    public Date? BirthDate { get; init; }

    /// <summary>
    /// Raised when an institution user has been updated
    /// </summary>
    public InstitutionUserUpdatedDomainEvent(InstitutionUser institutionUser)
    {
        UserId = institutionUser.Id;
        Phone = institutionUser.Phone;
        FirstName = institutionUser.FirstName;
        MiddleName = institutionUser.MiddleName;
        LastName = institutionUser.LastName;
        Role = institutionUser.Role;
        Address = institutionUser.Address;
        BirthDate = institutionUser.BirthDate;
    }
}