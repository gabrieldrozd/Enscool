using Core.Domain.DomainEvents;
using MediatR;
using Modules.Management.Application.Features.Institutions.InternalCommands.CreateInstitution;
using Modules.Management.Domain.Users.DomainEvents;

namespace Modules.Management.Messaging.DomainEvents.Users;

internal sealed class InstitutionAdminRegisteredDomainEventHandler(ISender sender)
    : IDomainEventHandler<InstitutionAdminRegisteredDomainEvent>
{
    public async Task Handle(InstitutionAdminRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var command = CreateInstitutionInternalCommand.From(notification);
        await sender.Send(command, cancellationToken);
    }
}