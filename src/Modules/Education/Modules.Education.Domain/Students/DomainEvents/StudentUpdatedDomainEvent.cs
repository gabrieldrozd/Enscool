using Core.Domain.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Education.Domain.Students.DomainEvents;

/// <summary>
/// Raised when a student has been updated
/// </summary>
public sealed record StudentUpdatedDomainEvent : IDomainEvent
{
    public UserId UserId { get; init; }
    public string Phone { get; init; }
    public string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public string LastName { get; init; }
    public Date? BirthDate { get; init; }
    public Address? Address { get; init; }
    public LanguageLevel? LanguageLevel { get; init; }

    /// <summary>
    /// Raised when a student has been updated
    /// </summary>
    public StudentUpdatedDomainEvent(Student student)
    {
        UserId = student.Id;
        Phone = student.Phone;
        FirstName = student.FirstName;
        MiddleName = student.MiddleName;
        LastName = student.LastName;
        BirthDate = student.BirthDate;
        Address = student.Address;
        LanguageLevel = student.LanguageLevel;
    }
}