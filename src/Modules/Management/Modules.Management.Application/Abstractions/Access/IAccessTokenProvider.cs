using Core.Application.Auth;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions.Access;

public interface IAccessTokenProvider
{
    /// <summary>Creates a new <see cref="AccessModel"/> for the given <see cref="User"/>.</summary>
    string Create(User user);
}