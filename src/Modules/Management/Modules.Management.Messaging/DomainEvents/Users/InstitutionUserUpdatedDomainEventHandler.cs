using Core.Application.Communication.External.Messages;
using Core.Domain.DomainEvents;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Payloads;
using Core.Messaging.Users.Students;
using MediatR;
using Modules.Management.Domain.Users.DomainEvents;

namespace Modules.Management.Messaging.DomainEvents.Users;

internal sealed class InstitutionUserUpdatedDomainEventHandler : IDomainEventHandler<InstitutionUserUpdatedDomainEvent>
{
    private readonly ISender _sender;
    private readonly IMessageBus _messageBus;

    public InstitutionUserUpdatedDomainEventHandler(ISender sender, IMessageBus messageBus)
    {
        _sender = sender;
        _messageBus = messageBus;
    }

    public async Task Handle(InstitutionUserUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        switch (notification.Role)
        {
            case UserRole.Student:
                await _messageBus.PublishAsync(new StudentUserUpdatedMessage
                {
                    UserId = notification.UserId,
                    Phone = notification.Phone,
                    FirstName = notification.FirstName,
                    MiddleName = notification.MiddleName,
                    LastName = notification.LastName,
                    Address = AddressPayload.From(notification.Address!),
                    BirthDate = notification.BirthDate!
                }, cancellationToken);
                break;
            case UserRole.Teacher:
            //TODO
            // await _messageBus.PublishAsync(new TeacherUserUpdatedMessage(
            //     new TeacherUserUpdatedMessagePayload
            //     {
            //         UserId = notification.UserId,
            //         Phone = notification.Phone,
            //         FirstName = notification.FirstName,
            //         MiddleName = notification.MiddleName,
            //         LastName = notification.LastName,
            //         BirthDate = notification.BirthDate!,
            //         Address = AddressPayload.From(notification.Address!)
            //     }), cancellationToken);
            // break;
            case UserRole.Secretary:
            case UserRole.InstitutionAdmin:
            case UserRole.Support:
            case UserRole.BackOfficeAdmin:
            case UserRole.GlobalAdmin:
            case UserRole.System:
            default:
                return;
        }
    }
}