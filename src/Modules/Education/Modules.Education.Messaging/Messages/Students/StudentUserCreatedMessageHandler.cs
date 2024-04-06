using Core.Application.Communication.External.Messages;
using Core.Messaging.Users;
using MediatR;
using Modules.Education.Application.Features.Students.InternalCommands.CreateStudent;

namespace Modules.Education.Messaging.Messages.Students;

internal sealed class StudentUserCreatedMessageHandler
    : IMessageHandler<StudentUserCreatedMessage>
{
    private readonly ISender _sender;

    public StudentUserCreatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(StudentUserCreatedMessage notification, CancellationToken cancellationToken)
    {
        await _sender.Send(new CreateStudentInternalCommand(
            notification.Payload.UserId,
            notification.Payload.State,
            notification.Payload.Email,
            notification.Payload.Phone,
            notification.Payload.FullName,
            notification.Payload.BirthDate,
            notification.Payload.Address,
            notification.Payload.LanguageLevel,
            notification.Payload.InstitutionId
        ), cancellationToken);
    }
}