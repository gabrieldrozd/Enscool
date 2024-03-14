using Core.Application.Auth;
using Core.Application.Communication.Internal.Queries;

namespace Modules.Management.Application.Features.Users.Queries;

public sealed record GetCurrentUserQuery : IQuery<AccessToken>;