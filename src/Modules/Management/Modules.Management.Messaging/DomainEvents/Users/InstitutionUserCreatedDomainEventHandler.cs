using Core.Application.Communication.External.Messages;
using Core.Domain.DomainEvents;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Messaging.Users;
using MediatR;
using Modules.Management.Domain.Users.DomainEvents;

namespace Modules.Management.Messaging.DomainEventHandlers.Users;

internal sealed class InstitutionUserCreatedDomainEventHandler(ISender sender, IMessageBus messageBus)
    : IDomainEventHandler<InstitutionUserCreatedDomainEvent>
{
    public async Task Handle(InstitutionUserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Role is UserRole.Student)
        {
            await messageBus.PublishAsync(new StudentUserCreatedMessage(
                new StudentUserCreatedMessagePayload
                {
                    UserId = notification.UserId,
                    State = notification.State,
                    Email = notification.Email,
                    Phone = notification.Phone,
                    FullName = notification.FullName,
                    BirthDate = notification.BirthDate!,
                    Address = notification.Address!,
                    LanguageLevel = notification.LanguageLevel!,
                    InstitutionId = notification.InstitutionId
                }), cancellationToken);
        }
    }
}