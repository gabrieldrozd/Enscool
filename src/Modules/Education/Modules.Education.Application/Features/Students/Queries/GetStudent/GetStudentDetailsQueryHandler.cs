using Common.Utilities.Primitives.Results;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Queries;
using Microsoft.EntityFrameworkCore;
using Modules.Education.Application.Abstractions;

namespace Modules.Education.Application.Features.Students.Queries.GetStudent;

internal sealed class GetStudentDetailsQueryHandler : IQueryHandler<GetStudentDetailsQuery, GetStudentDetailsQueryDto>
{
    private readonly IEducationDbContext _context;

    public GetStudentDetailsQueryHandler(IEducationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetStudentDetailsQueryDto>> Handle(GetStudentDetailsQuery request, CancellationToken cancellationToken)
    {
        var institution = await _context.Students
            .Where(x => x.Id == request.StudentId)
            .AsNoTracking()
            .Select(GetStudentDetailsQueryDto.Mapper)
            .SingleOrDefaultAsync(cancellationToken);

        return institution ?? Result.Failure.NotFound<GetStudentDetailsQueryDto>(Resource.UserNotFound, request.StudentId);
    }
}