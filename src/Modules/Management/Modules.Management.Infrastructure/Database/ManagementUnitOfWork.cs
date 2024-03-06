using Core.Infrastructure.Database.UnitOfWork;
using Modules.Management.Application.Abstractions;

namespace Modules.Management.Infrastructure.Database;

internal sealed class ManagementUnitOfWork : BaseUnitOfWork<ManagementDbContext>, IUnitOfWork
{
    public ManagementUnitOfWork(ManagementDbContext context)
        : base(context)
    {
    }
}