using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Core.Domain.Primitives;

namespace Core.Domain.Shared.ValueObjects;

public sealed class Address : ValueObject
{
    private const string ZipCodeRegexPattern = "^[0-9]{2}[-\\s]*?[0-9]{3}$";

    public string ZipCode { get; }
    public string ZipCodeCity { get; }
    public string City { get; }
    public string HouseNumber { get; }
    public string? State { get; }
    public string? Street { get; }

    private Address(string zipCode, string zipCodeCity, string city, string houseNumber, string? state, string? street)
    {
        Ensure.Not.InvalidFormat(zipCode, ZipCodeRegexPattern);
        Ensure.Not.NullOrEmpty(zipCode);
        Ensure.Not.NullOrEmpty(zipCodeCity);
        Ensure.Not.NullOrEmpty(city);
        Ensure.Not.NullOrEmpty(houseNumber);

        ZipCode = zipCode;
        ZipCodeCity = zipCodeCity;
        City = city;
        HouseNumber = houseNumber;
        State = string.IsNullOrWhiteSpace(state) ? null : state.Trim();
        Street = string.IsNullOrWhiteSpace(street) ? null : street.Trim();
    }

    public static Address Create(
        string zipCode,
        string zipCodeCity,
        string city,
        string houseNumber,
        string? state = null,
        string? street = null
    ) => new(zipCode, zipCodeCity, city, houseNumber, state, street);

    public static implicit operator string(Address address) => address.ToString();

    public override string ToString() => !string.IsNullOrWhiteSpace(Street)
        ? $"{ZipCode} {ZipCodeCity}, {City} {Street} {HouseNumber}"
        : $"{ZipCode} {ZipCodeCity}, {City} {HouseNumber}";

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return ZipCode;
        yield return ZipCodeCity;
        yield return City;
        yield return HouseNumber;

        if (State != null)
            yield return State;
        if (Street != null)
            yield return Street;
    }
}