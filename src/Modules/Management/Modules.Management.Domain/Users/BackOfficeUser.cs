using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users;

public class BackOfficeUser : User
{
    private BackOfficeUser()
    {
    }

    public BackOfficeUser(UserId id, Email email, Phone phone, string firstName, string? middleName, string lastName, BackOfficeUserRole role, List<InstitutionId> institutionIds)
        : base(id, email, phone, firstName, middleName, lastName, role.ToUserRole(), null, institutionIds)
    {
    }
}