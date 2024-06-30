using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Teachers;
using MediatR;
using Modules.Education.Application.Features.Teachers.InternalCommands.DeleteTeacher;

namespace Modules.Education.Messaging.Messages.Teachers;

internal sealed class TeacherUserDeletedMessageHandler : IMessageHandler<TeacherUserDeletedMessage>
{
    private readonly ISender _sender;

    public TeacherUserDeletedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(TeacherUserDeletedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new DeleteTeacherInternalCommand(notification.UserId), cancellationToken);
}