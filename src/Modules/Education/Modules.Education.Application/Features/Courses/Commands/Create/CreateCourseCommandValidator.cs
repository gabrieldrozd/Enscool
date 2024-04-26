using FluentValidation;

namespace Modules.Education.Application.Features.Courses.Commands.Create;

internal sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Level).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.MainTeacherId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.StudentIds).NotEmpty();
        // RuleFor(x => x.CourseTypeId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.InstitutionId).NotEmpty().NotEqual(Guid.Empty);
    }
}