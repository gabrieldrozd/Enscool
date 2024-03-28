using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Users.Commands.RefreshAccess;

namespace Modules.Management.Api.Endpoints.Access.Refresh;

internal sealed class RefreshAccessRequest : IWithMapTo<RefreshAccessCommand>
{
    public required Guid UserId { get; init; }
    public required string RefreshToken { get; init; }

    public RefreshAccessCommand Map() => new(UserId, RefreshToken);
}