using Common.Utilities.Abstractions;
using Core.Domain.Primitives;

namespace Core.Application.Database;

public interface IRepository<in TEntity> : IRepository
    where TEntity : class, IEntity
{
    void Insert(TEntity entity);
    void InsertRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}