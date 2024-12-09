using Food.App.Core.Entities;
using System.Linq.Expressions;

namespace Food.App.Core.Interfaces;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void SaveInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
    void SaveIncludeRange(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);
    void UpdateFullEntity(IEnumerable<TEntity> entities);
    void SaveExclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllWithDeleted();
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
    IQueryable<TEntity> AsQuerable();

    Task<TEntity?> GetByIdAsync(int id);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    void Undelete(TEntity entity);
    void HardDelete(TEntity entity);

    public IQueryable<TEntity> GetAllWithInclude(Func<IQueryable<TEntity>, IQueryable<TEntity>> Expr);
    Task<bool> DoesEntityExistAsync(int id);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

    //Task SaveChangesAsync();
}
