using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Teachers;
using MediatR;
using Modules.Education.Application.Features.Teachers.InternalCommands.DeactivateTeacher;

namespace Modules.Education.Messaging.Messages.Teachers;

internal sealed class TeacherUserDeactivatedMessageHandler : IMessageHandler<TeacherUserDeactivatedMessage>
{
    private readonly ISender _sender;

    public TeacherUserDeactivatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(TeacherUserDeactivatedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new DeactivateTeacherInternalCommand(notification.UserId), cancellationToken);
}