using Core.Application.Communication.External.Messages;
using Core.Messaging.Users.Students;
using MediatR;
using Modules.Education.Application.Features.Students.InternalCommands.RestoreStudent;

namespace Modules.Education.Messaging.Messages.Students;

internal sealed class StudentUserRestoredMessageHandler : IMessageHandler<StudentUserRestoredMessage>
{
    private readonly ISender _sender;

    public StudentUserRestoredMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(StudentUserRestoredMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new RestoreStudentInternalCommand(notification.UserId), cancellationToken);
}