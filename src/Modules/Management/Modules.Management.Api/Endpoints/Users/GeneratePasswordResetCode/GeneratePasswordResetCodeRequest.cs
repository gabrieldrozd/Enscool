using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Users.Commands.GeneratePasswordResetCode;

namespace Modules.Management.Api.Endpoints.Users.GeneratePasswordResetCode;

internal sealed class GeneratePasswordResetCodeRequest : IWithMapTo<GeneratePasswordResetCodeCommand>
{
    public required string Email { get; init; }

    public GeneratePasswordResetCodeCommand Map() => new(Email);
}