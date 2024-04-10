using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users;

public class BackOfficeUser : User
{
    private BackOfficeUser()
    {
    }

    public BackOfficeUser(UserId id, Email email, Phone phone, FullName fullName, BackOfficeUserRole role, List<InstitutionId> institutionIds)
        : base(id, email, phone, fullName, role.ToUserRole(), null, institutionIds)
    {
    }
}