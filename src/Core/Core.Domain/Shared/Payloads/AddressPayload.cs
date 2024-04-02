using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.ValueObjects;

namespace Core.Domain.Shared.Payloads;

public sealed class AddressPayload
    : IWithMapTo<Address>
{
    public string ZipCode { get; init; } = null!;
    public string ZipCodeCity { get; init; } = null!;
    public string City { get; init; } = null!;
    public string HouseNumber { get; init; } = null!;
    public string? State { get; init; }
    public string? Street { get; init; }

    public Address Map() => Address.Create(ZipCode, ZipCodeCity, City, HouseNumber, State, Street);
}