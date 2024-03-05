using System.Linq.Expressions;
using Core.Application.Abstractions.Database;
using Core.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Database.Repositories;

public class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : class, IEntity
    where TDbContext : DbContext
{
    private readonly TDbContext _context;

    protected Repository(TDbContext context) => _context = context;

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => _context.Set<TEntity>().AnyAsync(predicate, cancellationToken);

    public void Insert(TEntity entity)
        => _context.Set<TEntity>().Add(entity);

    public void InsertRange(IEnumerable<TEntity> entities)
        => _context.Set<TEntity>().AddRange(entities);

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);
}