using Core.Application.Auth;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions.Services;

public interface ITokenService
{
    /// <summary>Creates a new <see cref="AccessToken"/> for the given <see cref="User"/>.</summary>
    AccessToken Create(User user);
}