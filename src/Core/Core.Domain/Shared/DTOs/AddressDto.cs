using System.Linq.Expressions;
using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.ValueObjects;

namespace Core.Domain.Shared.DTOs;

public sealed class AddressDto
    : IWithExpressionMapFrom<Address, AddressDto>,
      IWithMapFrom<Address, AddressDto>,
      IWithMapFromNullable<Address, AddressDto>
{
    public string ZipCode { get; init; } = null!;
    public string ZipCodeCity { get; init; } = null!;
    public string City { get; init; } = null!;
    public string HouseNumber { get; init; } = null!;
    public string? State { get; init; }
    public string? Street { get; init; }

    public static Expression<Func<Address, AddressDto>> GetMapping() =>
        address => new AddressDto
        {
            ZipCode = address.ZipCode,
            ZipCodeCity = address.ZipCodeCity,
            City = address.City,
            HouseNumber = address.HouseNumber,
            State = address.State,
            Street = address.Street
        };

    public static AddressDto From(Address source)
        => new()
        {
            ZipCode = source.ZipCode,
            ZipCodeCity = source.ZipCodeCity,
            City = source.City,
            HouseNumber = source.HouseNumber,
            State = source.State,
            Street = source.Street
        };

    public static AddressDto? FromNullable(Address? source) => source is not null ? From(source) : null;
}