using Core.Application.Auth;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions.Access;

public interface ITokenProvider
{
    /// <summary>Creates a new <see cref="AccessModel"/> for the given <see cref="User"/>.</summary>
    AccessModel Create(User user);
}