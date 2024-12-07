using Food.App.Core.Entities;

namespace Food.App.Core.Interfaces;
public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    Task<int> SaveChangesAsync();
}
