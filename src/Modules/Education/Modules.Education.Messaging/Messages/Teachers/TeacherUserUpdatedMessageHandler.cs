using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Teachers;
using MediatR;
using Modules.Education.Application.Features.Teachers.InternalCommands.UpdateTeacher;

namespace Modules.Education.Messaging.Messages.Teachers;

internal sealed class TeacherUserUpdatedMessageHandler : IMessageHandler<TeacherUserUpdatedMessage>
{
    private readonly ISender _sender;

    public TeacherUserUpdatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(TeacherUserUpdatedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new UpdateTeacherInternalCommand(
            notification.UserId,
            notification.Phone,
            notification.FirstName,
            notification.MiddleName,
            notification.LastName,
            notification.Address.Map()
        ), cancellationToken);
}