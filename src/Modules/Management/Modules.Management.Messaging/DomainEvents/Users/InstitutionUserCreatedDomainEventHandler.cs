using Core.Application.Communication.External.Messages;
using Core.Domain.DomainEvents;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Messaging.Users;
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
        if (notification.Role is UserRole.Student)
        {
            await _messageBus.PublishAsync(new StudentUserCreatedMessage(
                new StudentUserCreatedMessagePayload
                {
                    UserId = notification.UserId,
                    State = notification.State,
                    Email = notification.Email,
                    Phone = notification.Phone,
                    FullName = new FullNamePayload
                    {
                        First = notification.FullName.First,
                        Middle = notification.FullName.Middle,
                        Last = notification.FullName.Last
                    },
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
        }

        if (notification.Role is UserRole.Teacher)
        {
            // TODO: Publish message for teacher user
        }
    }
}