using Core.Application.Abstractions.Auth;
using Core.Domain.Primitives;
using Core.Domain.Shared.Defaults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Core.Infrastructure.Database.Interceptors;

public sealed class EntityDeletedSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext _userContext;

    public EntityDeletedSaveChangesInterceptor(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context == null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        foreach (var entry in eventData.Context.ChangeTracker.Entries<IEntity>())
            ProcessEntry(entry);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ProcessEntry(EntityEntry<IEntity> entry)
    {
        if (entry.State is not EntityState.Modified)
            return;

        var isDeletedModified = entry.Property(nameof(IEntity.Deleted)).IsModified;
        if (!isDeletedModified)
            return;

        var isDeleted = entry.Entity.Deleted;
        var deletedBy = _userContext.UserId ?? SystemUser.Id;
        entry.Entity.SetDeletedBy(isDeleted ? deletedBy : null);
    }
}