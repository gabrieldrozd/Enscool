using System.Linq.Expressions;
using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.ValueObjects;

namespace Core.Domain.Shared.DTOs;

public sealed class AddressDto
    : IWithExpressionMapFrom<Address, AddressDto>,
      IWithMapFrom<Address, AddressDto>
{
    public string ZipCode { get; init; } = null!;
    public string ZipCodeCity { get; init; } = null!;
    public string City { get; init; } = null!;
    public string HouseNumber { get; init; } = null!;
    public string? State { get; init; }
    public string? Street { get; init; }

    public static Expression<Func<Address, AddressDto>> Mapper =>
        address => new AddressDto
        {
            ZipCode = address.ZipCode,
            ZipCodeCity = address.ZipCodeCity,
            City = address.City,
            HouseNumber = address.HouseNumber,
            State = address.State,
            Street = address.Street
        };

    public static AddressDto From(Address source) => Mapper.Compile().Invoke(source);
}