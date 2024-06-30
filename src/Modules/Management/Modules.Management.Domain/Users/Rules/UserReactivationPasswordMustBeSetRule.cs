using Core.Domain.Primitives.Rules;

namespace Modules.Management.Domain.Users.Rules;

public class UserReactivationPasswordMustBeSetRule : IBusinessRule
{
    private readonly bool _hasPassword;

    public UserReactivationPasswordMustBeSetRule(bool hasPassword) => _hasPassword = hasPassword;

    public string Message => "User activation requires password.";
    public bool IsInvalid() => !_hasPassword;
}