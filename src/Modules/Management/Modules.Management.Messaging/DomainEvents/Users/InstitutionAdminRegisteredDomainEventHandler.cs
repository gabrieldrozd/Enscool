using Core.Domain.DomainEvents;
using MediatR;
using Modules.Management.Application.Features.Institutions.InternalCommands.CreateInstitution;
using Modules.Management.Domain.Users.DomainEvents;

namespace Modules.Management.Messaging.DomainEvents.Users;

internal sealed class InstitutionAdminRegisteredDomainEventHandler
    : IDomainEventHandler<InstitutionAdminRegisteredDomainEvent>
{
    private readonly ISender _sender;

    public InstitutionAdminRegisteredDomainEventHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(InstitutionAdminRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var command = CreateInstitutionInternalCommand.From(notification);
        await _sender.Send(command, cancellationToken);
    }
}