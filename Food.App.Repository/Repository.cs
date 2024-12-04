using Food.App.Core.Entities;
using Food.App.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Food.App.Repository;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _dbSet = _appDbContext.Set<TEntity>();
    }


    public async Task AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        //entity.CreatedBy=userId;

        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedAt = DateTime.Now;

        }
        await _dbSet.AddRangeAsync(entities);
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        //entity.DeletedBy = User.id;
        SaveInclude(entity, p => p.IsDeleted, p => p.DeletedAt);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            Delete(entity);
        }
    }


    public void Undelete(TEntity entity)
    {
        entity.IsDeleted = false;
        entity.DeletedAt = null;
        //entity.DeletedBy = null;
        SaveInclude(entity, p => p.IsDeleted, p => p.DeletedAt);
    }

    public void HardDelete(TEntity entity) => _dbSet.Remove(entity);
    public IQueryable<TEntity> GetAll() => _dbSet.Where(e => !e.IsDeleted);
    public IQueryable<TEntity> GetAllWithDeleted() => _dbSet;
    public async Task<TEntity?> GetByIdAsync(int id) => await GetAll().FirstOrDefaultAsync(x => x.Id == id);
    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression) => GetAll().Where(expression);


    //public async Task SaveChangesAsync() => await _appDbContext.SaveChangesAsync();



    public void SaveInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
    {
        var local = _dbSet.Local.FirstOrDefault(x => x.Id == entity.Id);

        EntityEntry<TEntity> entityEntry = null;

        if (local is null)
            entityEntry = _appDbContext.Entry(entity);
        else
            entityEntry = _appDbContext.ChangeTracker.Entries<TEntity>()
                .First(x => x.Entity.Id == entity.Id);

        foreach (var propertyExpression in propertyExpressions)
        {
            var propertyToUpdate = entityEntry.Property(propertyExpression);
            var propertyName = propertyToUpdate.Metadata.Name;
            propertyToUpdate.CurrentValue = entity.GetType().GetProperty(propertyName)?.GetValue(entity);
            propertyToUpdate.IsModified = true;
        }
    }

    public void SaveExclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
    {
        var propertiesToExclude = new HashSet<string>(propertyExpressions.Select(e => ((MemberExpression)e.Body).Member.Name));

        var local = _dbSet.Local.FirstOrDefault(i => i.Id == entity.Id);
        EntityEntry<TEntity> entityEntry = null;
        if (local is null)
            entityEntry = _appDbContext.Entry(entity);
        else
            entityEntry = _appDbContext.ChangeTracker.Entries<TEntity>().First(i => i.Entity.Id == entity.Id);

        foreach (var propertyEntry in entityEntry.Properties)
        {
            var propertyName = propertyEntry.Metadata.Name;

            if (propertiesToExclude.Contains(propertyName))
                propertyEntry.IsModified = false;
            else
                propertyEntry.IsModified = true;
        }
    }

    public void SaveIncludeRange(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
    {
        foreach (var entity in entities)
        {
            SaveInclude(entity, propertyExpressions);
        }
    }

    public async Task<bool> DoesEntityExistAsync(int id) => await _dbSet.AnyAsync(e => e.Id == id);
}
