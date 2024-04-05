using FluentValidation;

namespace Modules.Education.Application.Features.Students.Queries.GetStudent;

internal sealed class GetStudentDetailsQueryValidator : AbstractValidator<GetStudentDetailsQuery>
{
    public GetStudentDetailsQueryValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty().NotEqual(Guid.Empty);
    }
}