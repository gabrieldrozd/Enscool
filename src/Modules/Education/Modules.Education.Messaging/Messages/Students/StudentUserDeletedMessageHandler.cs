using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Students;
using MediatR;
using Modules.Education.Application.Features.Students.InternalCommands.DeleteStudent;

namespace Modules.Education.Messaging.Messages.Students;

internal sealed class StudentUserDeletedMessageHandler : IMessageHandler<StudentUserDeletedMessage>
{
    private readonly ISender _sender;

    public StudentUserDeletedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(StudentUserDeletedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new DeleteStudentInternalCommand(notification.Payload.UserId), cancellationToken);
}