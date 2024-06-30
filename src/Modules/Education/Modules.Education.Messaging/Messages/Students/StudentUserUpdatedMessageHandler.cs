using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.ValueObjects;
using Core.Messaging.Users.Students;
using MediatR;
using Modules.Education.Application.Features.Students.InternalCommands.UpdateStudent;

namespace Modules.Education.Messaging.Messages.Students;

internal sealed class StudentUserUpdatedMessageHandler : IMessageHandler<StudentUserUpdatedMessage>
{
    private readonly ISender _sender;

    public StudentUserUpdatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(StudentUserUpdatedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new UpdateStudentInternalCommand(
            notification.UserId,
            notification.Phone,
            notification.FirstName,
            notification.MiddleName,
            notification.LastName,
            Date.Create(notification.BirthDate),
            notification.Address.Map()
        ), cancellationToken);
}