using Domain.Entities.BaseEntities;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : IEntity
{
    // Queries

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    // Commands
    void Add(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
}