using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Students;
using MediatR;
using Modules.Education.Application.Features.Students.InternalCommands.DeactivateStudent;

namespace Modules.Education.Messaging.Messages.Students;

internal sealed class StudentUserDeactivatedMessageHandler : IMessageHandler<StudentUserDeactivatedMessage>
{
    private readonly ISender _sender;

    public StudentUserDeactivatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(StudentUserDeactivatedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new DeactivateStudentInternalCommand(notification.UserId), cancellationToken);
}