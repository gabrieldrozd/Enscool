using Core.Infrastructure.Database.UnitOfWork;
using Microsoft.Extensions.Logging;
using Modules.Management.Application.Abstractions;

namespace Modules.Management.Infrastructure.Database;

internal sealed class ManagementUnitOfWork : BaseUnitOfWork<ManagementDbContext>, IUnitOfWork
{
    public ManagementUnitOfWork(ManagementDbContext context)
        : base(context)
    {
    }
}