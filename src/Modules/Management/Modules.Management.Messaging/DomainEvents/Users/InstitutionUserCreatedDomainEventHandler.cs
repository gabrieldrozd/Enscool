using Core.Application.Communication.External.Messages;
using Core.Domain.DomainEvents;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Messaging.Users.Students;
using Core.Messaging.Users.Teachers;
using MediatR;
using Modules.Management.Domain.Users.DomainEvents;

namespace Modules.Management.Messaging.DomainEvents.Users;

internal sealed class InstitutionUserCreatedDomainEventHandler : IDomainEventHandler<InstitutionUserCreatedDomainEvent>
{
    private readonly ISender _sender;
    private readonly IMessageBus _messageBus;

    public InstitutionUserCreatedDomainEventHandler(ISender sender, IMessageBus messageBus)
    {
        _sender = sender;
        _messageBus = messageBus;
    }

    public async Task Handle(InstitutionUserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        switch (notification.Role)
        {
            case UserRole.Student:
                await _messageBus.PublishAsync(new StudentUserCreatedMessage(
                    new StudentUserCreatedMessagePayload
                    {
                        UserId = notification.UserId,
                        State = notification.State,
                        Email = notification.Email,
                        Phone = notification.Phone,
                        FirstName = notification.FirstName,
                        MiddleName = notification.MiddleName,
                        LastName = notification.LastName,
                        BirthDate = notification.BirthDate!,
                        Address = new AddressPayload
                        {
                            ZipCode = notification.Address!.ZipCode,
                            ZipCodeCity = notification.Address!.ZipCodeCity,
                            City = notification.Address!.City,
                            HouseNumber = notification.Address!.HouseNumber,
                            State = notification.Address!.State,
                            Street = notification.Address!.Street
                        },
                        LanguageLevel = notification.LanguageLevel!,
                        InstitutionId = notification.InstitutionId
                    }), cancellationToken);
                break;
            case UserRole.Teacher:
                await _messageBus.PublishAsync(new TeacherUserCreatedMessage(
                    new TeacherUserCreatedMessagePayload
                    {
                        UserId = notification.UserId,
                        State = notification.State,
                        Email = notification.Email,
                        Phone = notification.Phone,
                        FirstName = notification.FirstName,
                        MiddleName = notification.MiddleName,
                        LastName = notification.LastName,
                        Address = new AddressPayload
                        {
                            ZipCode = notification.Address!.ZipCode,
                            ZipCodeCity = notification.Address!.ZipCodeCity,
                            City = notification.Address!.City,
                            HouseNumber = notification.Address!.HouseNumber,
                            State = notification.Address!.State,
                            Street = notification.Address!.Street
                        },
                        InstitutionId = notification.InstitutionId
                    }), cancellationToken);
                break;
            case UserRole.Secretary:
            case UserRole.InstitutionAdmin:
            case UserRole.Support:
            case UserRole.BackOfficeAdmin:
            case UserRole.GlobalAdmin:
            case UserRole.System:
            default:
                return;
        }
    }
}