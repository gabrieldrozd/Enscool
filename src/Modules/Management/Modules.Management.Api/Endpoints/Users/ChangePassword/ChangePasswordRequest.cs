using Common.Utilities.Models;
using Modules.Management.Application.Features.Users.Commands.ChangePassword;

namespace Modules.Management.Api.Endpoints.Users.ChangePassword;

internal sealed class ChangePasswordRequest : IWithMapTo<ChangePasswordCommand>
{
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }

    public ChangePasswordCommand Map() => new(OldPassword, NewPassword);
}