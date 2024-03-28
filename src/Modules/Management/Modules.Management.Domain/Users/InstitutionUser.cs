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

    // TODO: LanguageLevel non-nullable for Students (Role == UserRole.Student)
    public LanguageLevel? LanguageLevel { get; private set; }

    // TODO: Add EducationDetails ValueObject
    // public EducationDetails? Education { get; private set; }

    private InstitutionUser()
    {
    }

    private InstitutionUser(UserId id, Email email, Phone phone, FullName fullName, InstitutionUserRole role, InstitutionId institutionId)
        : base(id, email, phone, fullName, role.ToUserRole(), institutionId)
    {
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
}