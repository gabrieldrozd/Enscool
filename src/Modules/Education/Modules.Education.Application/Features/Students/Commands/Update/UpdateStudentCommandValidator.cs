namespace Modules.Education.Application.Features.Students.Commands.Update;

// internal sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
// {
//     public UpdateStudentCommandValidator()
//     {
//         RuleFor(x => x.UserId).NotNull().NotEqual(Guid.Empty);
//         RuleFor(x => x.Phone).NotEmpty();
//         RuleFor(x => x.FirstName).NotEmpty();
//         RuleFor(x => x.LastName).NotEmpty();
//         RuleFor(x => x.Address).NotNull().SetValidator(new AddressPayloadValidator());
//         RuleFor(x => x.LanguageLevel).NotEmpty().SetValidator(new LanguageLevelValidator());
//         RuleFor(x => x.BirthDate).NotEmpty();
//     }
// }