using Food.App.Core.Entities;
using System.Linq.Expressions;

namespace Food.App.Core.Interfaces;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void SaveInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
    void SaveIncludeRange(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);
    void SaveExclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllWithDeleted();
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> GetByIdAsync(int id);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    void Undelete(TEntity entity);
    void HardDelete(TEntity entity);
    Task<bool> DoesEntityExistAsync(int id);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

    //Task SaveChangesAsync();
}
