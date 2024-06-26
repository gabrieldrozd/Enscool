using Core.Application.Communication.External.Messages;
using Core.Domain.DomainEvents;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Messaging.Users.Students;
using Core.Messaging.Users.Teachers;
using MediatR;
using Modules.Management.Domain.Users.DomainEvents;

namespace Modules.Management.Messaging.DomainEvents.Users;

internal sealed class UserDeletedDomainEventHandler : IDomainEventHandler<UserDeletedDomainEvent>
{
    private readonly ISender _sender;
    private readonly IMessageBus _messageBus;

    public UserDeletedDomainEventHandler(ISender sender, IMessageBus messageBus)
    {
        _sender = sender;
        _messageBus = messageBus;
    }

    public async Task Handle(UserDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        switch (notification.Role)
        {
            case UserRole.Student:
                await _messageBus.PublishAsync(new StudentUserDeletedMessage(new StudentUserDeletedMessagePayload { UserId = notification.UserId }), cancellationToken);
                break;
            case UserRole.Teacher:
                await _messageBus.PublishAsync(new TeacherUserDeletedMessage(new TeacherUserDeletedMessagePayload { UserId = notification.UserId }), cancellationToken);
                break;
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