namespace TaskManagement.Application.Common.Interfaces.Persistence.Base;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
    void Create(TEntity entity);
    void CreateMany(List<TEntity> entities);
    void Update(TEntity entity);
    void UpdateMany(List<TEntity> entities);
    Task SaveChanges(CancellationToken cancellationToken);
}