using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Domain.Users.DomainEvents;
using Modules.Management.Domain.Users.Rules;

namespace Modules.Management.Domain.Users;

public class InstitutionUser : User
{
    public Address? Address { get; private set; }
    public LanguageLevel? LanguageLevel { get; private set; }
    public Date? BirthDate { get; private set; }

    private InstitutionUser()
    {
    }

    private InstitutionUser(
        UserId id,
        Email email,
        Phone phone,
        string firstName,
        string? middleName,
        string lastName,
        InstitutionUserRole role,
        InstitutionId institutionId,
        Address? address = null,
        LanguageLevel? languageLevel = null,
        Date? birthDate = null
    )
        : base(id, email, phone, firstName, middleName, lastName, role.ToUserRole(), institutionId, [])
    {
        BirthDate = birthDate;
        Address = address;
        LanguageLevel = languageLevel;
    }

    /// <summary>
    /// Creates initial <see cref="UserRole.InstitutionAdmin"/> user.
    /// </summary>
    public static InstitutionUser CreateInitialInstitutionAdmin(Email email, Phone phone, string firstName, string? middleName, string lastName, ActivationCode activationCode)
    {
        var user = new InstitutionUser(UserId.New, email, phone, firstName, middleName, lastName, InstitutionUserRole.InstitutionAdmin, InstitutionId.New);
        user.AddActivationCode(activationCode);

        user.RaiseDomainEvent(new InstitutionAdminRegisteredDomainEvent(user));

        return user;
    }

    /// <summary>
    /// Creates institution user.
    /// </summary>
    public static InstitutionUser Create(
        Email email,
        Phone phone,
        string firstName,
        string? middleName,
        string lastName,
        InstitutionUserRole role,
        Address? address,
        LanguageLevel? languageLevel,
        Date? birthDate,
        InstitutionId institutionId,
        ActivationCode activationCode)
    {
        Validate(new InstitutionUserAddressRequiredRule(role, address));
        Validate(new StudentLanguageLevelRequiredRule(role, languageLevel));
        Validate(new StudentBirthDateRequiredRule(role, birthDate));

        var user = new InstitutionUser(UserId.New, email, phone, firstName, middleName, lastName, role, institutionId, address, languageLevel, birthDate);
        user.AddActivationCode(activationCode);

        user.RaiseDomainEvent(new InstitutionUserCreatedDomainEvent(user));

        return user;
    }

    public void Update(Phone phone, string firstName, string? middleName, string lastName, Address? address, Date? birthDate)
    {
        Phone = phone;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Address = address;
        BirthDate = birthDate;

        RaiseDomainEvent(new InstitutionUserUpdatedDomainEvent(this));
    }
}