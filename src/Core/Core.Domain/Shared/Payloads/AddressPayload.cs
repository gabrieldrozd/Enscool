using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.ValueObjects;

namespace Core.Domain.Shared.Payloads;

public sealed class AddressPayload
    : IWithMapTo<Address>,
      IWithMapFrom<Address, AddressPayload>
{
    public string ZipCode { get; init; } = null!;
    public string ZipCodeCity { get; init; } = null!;
    public string City { get; init; } = null!;
    public string HouseNumber { get; init; } = null!;
    public string? State { get; init; }
    public string? Street { get; init; }

    public Address Map() => Address.Create(ZipCode, ZipCodeCity, City, HouseNumber, State, Street);

    public static AddressPayload From(Address source)
        => new()
        {
            ZipCode = source.ZipCode,
            ZipCodeCity = source.ZipCodeCity,
            City = source.City,
            HouseNumber = source.HouseNumber,
            State = source.State,
            Street = source.Street
        };
}