using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.ValueObjects;
using Core.Messaging.Users;
using MediatR;
using Modules.Education.Application.Features.Teachers.InternalCommands.CreateTeacher;

namespace Modules.Education.Messaging.Messages.Teachers;

internal sealed class TeacherUserCreatedMessageHandler
    : IMessageHandler<TeacherUserCreatedMessage>
{
    private readonly ISender _sender;

    public TeacherUserCreatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(TeacherUserCreatedMessage notification, CancellationToken cancellationToken)
    {
        await _sender.Send(new CreateTeacherInternalCommand(
            notification.Payload.UserId,
            notification.Payload.State,
            notification.Payload.Email,
            notification.Payload.Phone,
            FullName.Create(notification.Payload.FullName.First, notification.Payload.FullName.Middle, notification.Payload.FullName.Last),
            Address.Create(
                notification.Payload.Address.ZipCode,
                notification.Payload.Address.ZipCodeCity,
                notification.Payload.Address.City,
                notification.Payload.Address.HouseNumber,
                notification.Payload.Address.State,
                notification.Payload.Address.Street),
            notification.Payload.InstitutionId
        ), cancellationToken);
    }
}