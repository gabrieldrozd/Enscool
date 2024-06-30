using Core.Domain.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.DomainEvents;

/// <summary>
/// Raised when an institution admin has been registered
/// </summary>
public sealed record InstitutionAdminRegisteredDomainEvent : IDomainEvent
{
    public UserId UserId { get; init; }
    public Email Email { get; init; }
    public Phone Phone { get; init; }
    public string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public string LastName { get; init; }
    public InstitutionId InstitutionId { get; init; }

    /// <summary>
    /// Raised when an institution admin has been registered
    /// </summary>
    public InstitutionAdminRegisteredDomainEvent(InstitutionUser institutionUser)
    {
        UserId = institutionUser.Id;
        Email = institutionUser.Email;
        Phone = institutionUser.Phone;
        FirstName = institutionUser.FirstName;
        MiddleName = institutionUser.MiddleName;
        LastName = institutionUser.LastName;
        InstitutionId = institutionUser.InstitutionId!;
    }
}