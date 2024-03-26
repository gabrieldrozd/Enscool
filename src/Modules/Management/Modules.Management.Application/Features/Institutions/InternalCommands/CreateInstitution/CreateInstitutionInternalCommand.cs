using Common.Utilities.Abstractions.Mapping;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Institutions;
using Modules.Management.Domain.Users.Events;

namespace Modules.Management.Application.Features.Institutions.InternalCommands.CreateInstitution;

/// <summary>
/// Creates new <see cref="Institution"/>.
/// </summary>
public sealed record CreateInstitutionInternalCommand(Guid UserId, Guid InstitutionId)
    : IInternalCommand,
      IWithMapFrom<InstitutionAdminRegisteredEvent, CreateInstitutionInternalCommand>
{
    public static CreateInstitutionInternalCommand From(InstitutionAdminRegisteredEvent source)
        => new(source.UserId, source.InstitutionId);
}