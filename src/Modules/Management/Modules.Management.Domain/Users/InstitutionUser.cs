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
        FullName fullName,
        InstitutionUserRole role,
        InstitutionId institutionId,
        Address? address = null,
        LanguageLevel? languageLevel = null,
        Date? birthDate = null
    )
        : base(id, email, phone, fullName, role.ToUserRole(), institutionId, [])
    {
        BirthDate = birthDate;
        Address = address;
        LanguageLevel = languageLevel;
    }

    /// <summary>
    /// Creates initial <see cref="UserRole.InstitutionAdmin"/> user.
    /// </summary>
    public static InstitutionUser CreateInitialInstitutionAdmin(Email email, Phone phone, FullName fullName, ActivationCode activationCode)
    {
        var user = new InstitutionUser(UserId.New, email, phone, fullName, InstitutionUserRole.InstitutionAdmin, InstitutionId.New);
        user.AddActivationCode(activationCode);

        user.RaiseDomainEvent(new InstitutionAdminRegisteredDomainEvent(user.Id, user.Email, user.Phone, user.FullName, user.InstitutionId!));

        return user;
    }

    /// <summary>
    /// Creates institution user.
    /// </summary>
    public static InstitutionUser Create(
        Email email,
        Phone phone,
        FullName fullName,
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

        var user = new InstitutionUser(UserId.New, email, phone, fullName, role, institutionId, address, languageLevel, birthDate);
        user.AddActivationCode(activationCode);

        user.RaiseDomainEvent(new InstitutionUserCreatedDomainEvent(
            user.Id,
            user.State,
            user.Email,
            user.Phone,
            user.FullName,
            user.Role,
            user.BirthDate,
            user.Address,
            user.LanguageLevel,
            user.InstitutionId!));

        return user;
    }
}