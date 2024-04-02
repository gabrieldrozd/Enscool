using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Access.Commands.GeneratePasswordResetCode;

namespace Modules.Management.Api.Endpoints.Access.GeneratePasswordResetCode;

internal sealed class GeneratePasswordResetCodeRequest : IWithMapTo<GeneratePasswordResetCodeCommand>
{
    public required string Email { get; init; }

    public GeneratePasswordResetCodeCommand Map() => new(Email);
}