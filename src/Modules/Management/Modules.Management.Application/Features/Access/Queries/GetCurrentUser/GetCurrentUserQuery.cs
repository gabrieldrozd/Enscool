using Core.Application.Communication.Internal.Queries.Base;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Queries.GetCurrentUser;

/// <summary>
/// Gets currently logged in <see cref="User"/>
/// </summary>
public sealed record GetCurrentUserQuery : IQuery<GetCurrentUserQueryDto>;