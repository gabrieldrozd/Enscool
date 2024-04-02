using Core.Infrastructure.Database.UnitOfWork;
using Modules.Education.Application.Abstractions;

namespace Modules.Education.Infrastructure.Database;

internal sealed class EducationUnitOfWork : BaseUnitOfWork<EducationDbContext>, IUnitOfWork
{
    public EducationUnitOfWork(EducationDbContext context)
        : base(context)
    {
    }
}