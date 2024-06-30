using Core.Domain.Shared.Payloads;
using FluentValidation;

namespace Core.Application.Validators;

public class AddressPayloadValidator : AbstractValidator<AddressPayload>
{
    public AddressPayloadValidator()
    {
        RuleFor(x => x.ZipCode).NotEmpty();
        RuleFor(x => x.ZipCodeCity).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.HouseNumber).NotEmpty();
    }
}