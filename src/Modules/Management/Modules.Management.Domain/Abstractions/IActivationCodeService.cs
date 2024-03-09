using Common.Utilities.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Domain.Abstractions;

public interface IActivationCodeService : IDomainService
{
    ActivationCode Generate();
}