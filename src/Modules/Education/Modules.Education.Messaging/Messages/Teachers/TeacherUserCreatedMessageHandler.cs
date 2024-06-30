using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.ValueObjects;
using Core.Messaging.Users.Teachers;
using MediatR;
using Modules.Education.Application.Features.Teachers.InternalCommands.CreateTeacher;

namespace Modules.Education.Messaging.Messages.Teachers;

internal sealed class TeacherUserCreatedMessageHandler
    : IMessageHandler<TeacherUserCreatedMessage>
{
    private readonly ISender _sender;

    public TeacherUserCreatedMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task Handle(TeacherUserCreatedMessage notification, CancellationToken cancellationToken)
        => await _sender.Send(new CreateTeacherInternalCommand(
            notification.UserId,
            notification.State,
            notification.Email,
            notification.Phone,
            notification.FirstName,
            notification.MiddleName,
            notification.LastName,
            notification.Address.Map(),
            notification.InstitutionId
        ), cancellationToken);
}