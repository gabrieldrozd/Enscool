using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Teachers;
using MediatR;
using Modules.Education.Application.Features.Teachers.InternalCommands.RestoreTeacher;

namespace Modules.Education.Messaging.Messages.Teachers;

internal sealed class TeacherUserRestoredMessageHandler : IMessageHandler<TeacherUserRestoredMessage>
{
    private readonly ISender _sender;

    public TeacherUserRestoredMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(TeacherUserRestoredMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new RestoreTeacherInternalCommand(notification.Payload.UserId), cancellationToken);
}