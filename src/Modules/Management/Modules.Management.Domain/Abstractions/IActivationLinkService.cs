using Common.Utilities.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Domain.Abstractions;

public interface IActivationLinkService : IDomainService
{
    string Create(User user);
}