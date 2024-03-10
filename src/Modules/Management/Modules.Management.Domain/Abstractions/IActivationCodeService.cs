using Modules.Management.Domain.Users;

namespace Modules.Management.Domain.Abstractions;

public interface IActivationCodeService
{
    ActivationCode Generate();
}