using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Access.Commands.ChangePassword;

namespace Modules.Management.Api.Endpoints.Access.ChangePassword;

internal sealed class ChangePasswordRequest : IWithMapTo<ChangePasswordCommand>
{
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }

    public ChangePasswordCommand Map() => new(OldPassword, NewPassword);
}