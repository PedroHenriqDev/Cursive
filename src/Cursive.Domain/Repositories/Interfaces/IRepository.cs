using Cursive.Domain.Entities;
using Cursive.Domain.Entities.Abstractions;

namespace Cursive.Domain.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> All { get; }
    IQueryable<TEntity> AllNotTracking { get; }
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity?> GetAsync(Predicate<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllNotTrackingAsync();
    Task CreateAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveAsync(TEntity entity);
}
