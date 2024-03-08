using System.Linq.Expressions;
using Core.Domain.Primitives;

namespace Core.Application.Database;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    void Insert(TEntity entity);
    void InsertRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}