using Core.Domain.Events;
using MediatR;
using Modules.Management.Application.Features.Institutions.InternalCommands.CreateInstitution;
using Modules.Management.Domain.Users.Events;

namespace Modules.Management.Messaging.Handlers.Users;

internal sealed class InstitutionAdminRegisteredHandler : IEventHandler<InstitutionAdminRegisteredEvent>
{
    private readonly ISender _sender;

    public InstitutionAdminRegisteredHandler(ISender sender) => _sender = sender;

    public async Task Handle(InstitutionAdminRegisteredEvent notification, CancellationToken cancellationToken)
    {
        await _sender.Send(CreateInstitutionInternalCommand.From(notification), cancellationToken);
    }
}