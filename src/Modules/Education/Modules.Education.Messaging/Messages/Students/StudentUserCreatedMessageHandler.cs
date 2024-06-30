using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.ValueObjects;
using Core.Messaging.Users.Students;
using MediatR;
using Modules.Education.Application.Features.Students.InternalCommands.CreateStudent;

namespace Modules.Education.Messaging.Messages.Students;

internal sealed class StudentUserCreatedMessageHandler : IMessageHandler<StudentUserCreatedMessage>
{
    private readonly ISender _sender;

    public StudentUserCreatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(StudentUserCreatedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new CreateStudentInternalCommand(
            notification.UserId,
            notification.State,
            notification.Email,
            notification.Phone,
            notification.FirstName,
            notification.MiddleName,
            notification.LastName,
            Date.Create(notification.BirthDate),
            notification.Address.Map(),
            notification.LanguageLevel,
            notification.InstitutionId
        ), cancellationToken);
}