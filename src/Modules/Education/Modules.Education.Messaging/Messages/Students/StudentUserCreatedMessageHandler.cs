using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.ValueObjects;
using Core.Messaging.Users.Students;
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
            notification.Payload.FirstName,
            notification.Payload.MiddleName,
            notification.Payload.LastName,
            Date.Create(notification.Payload.BirthDate),
            Address.Create(
                notification.Payload.Address.ZipCode,
                notification.Payload.Address.ZipCodeCity,
                notification.Payload.Address.City,
                notification.Payload.Address.HouseNumber,
                notification.Payload.Address.State,
                notification.Payload.Address.Street),
            notification.Payload.LanguageLevel,
            notification.Payload.InstitutionId
        ), cancellationToken);
    }
}