using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Domain.Users.Events;

namespace Modules.Management.Domain.Users;

public class InstitutionUser : User
{
    public Date? BirthDate { get; private set; }
    public Address? Address { get; private set; }
    public LanguageLevel? LanguageLevel { get; private set; }

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
        Date? birthDate = null,
        Address? address = null,
        LanguageLevel? languageLevel = null
    )
        : base(id, email, phone, fullName, role.ToUserRole(), institutionId)
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

        user.Raise(new InstitutionAdminRegisteredEvent(user.Id, user.Email, user.Phone, user.FullName, user.InstitutionId!));

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
        Date? birthDate,
        Address? address,
        LanguageLevel? languageLevel,
        InstitutionId institutionId)
    {
        // TODO: Add Rule - Each role except admin should have BirthDate and Address
        // TODO: Add Rule - Student role should have also LanguageLevel

        var user = new InstitutionUser(UserId.New, email, phone, fullName, role, institutionId, birthDate, address, languageLevel);

        user.Raise(new InstitutionUserCreatedEvent(
            user.Id,
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