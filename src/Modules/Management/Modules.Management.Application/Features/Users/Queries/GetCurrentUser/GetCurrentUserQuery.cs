using Core.Application.Communication.Internal.Queries;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Queries.GetCurrentUser;

/// <summary>
/// Gets currently logged in <see cref="User"/>
/// </summary>
public sealed record GetCurrentUserQuery : IQuery<UserDto>;