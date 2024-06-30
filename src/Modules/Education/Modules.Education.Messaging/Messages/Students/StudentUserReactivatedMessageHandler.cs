using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Students;
using MediatR;
using Modules.Education.Application.Features.Students.InternalCommands.ReactivateStudent;

namespace Modules.Education.Messaging.Messages.Students;

internal sealed class StudentUserReactivatedMessageHandler : IMessageHandler<StudentUserReactivatedMessage>
{
    private readonly ISender _sender;

    public StudentUserReactivatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(StudentUserReactivatedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new ReactivateStudentInternalCommand(notification.UserId), cancellationToken);
}