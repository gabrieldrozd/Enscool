using Common.Utilities.Resources;
using Core.Domain.Primitives.Rules;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.Rules;

public class InstitutionUserAddressRequiredRule : IBusinessRule
{
    private readonly InstitutionUserRole _role;
    private readonly Address? _address;

    public InstitutionUserAddressRequiredRule(InstitutionUserRole role, Address? address)
    {
        _role = role;
        _address = address;
    }

    public string Message => Resource.InstitutionUserAddressRequired;
    public bool IsInvalid() => _role is not InstitutionUserRole.InstitutionAdmin && _address is null;
}