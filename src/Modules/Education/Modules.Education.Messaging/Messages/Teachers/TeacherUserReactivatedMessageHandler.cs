using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Teachers;
using MediatR;
using Modules.Education.Application.Features.Teachers.InternalCommands.ReactivateTeacher;

namespace Modules.Education.Messaging.Messages.Teachers;

internal sealed class TeacherUserReactivatedMessageHandler : IMessageHandler<TeacherUserReactivatedMessage>
{
    private readonly ISender _sender;

    public TeacherUserReactivatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(TeacherUserReactivatedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new ReactivateTeacherInternalCommand(notification.UserId), cancellationToken);
}