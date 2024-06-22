namespace TaskManagement.Infrastructure.Persistence.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly TaskManagementContext _dbContext;

    protected BaseRepository(TaskManagementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable().AsNoTracking();

        if (where != null)
        {
            query = query.Where(where);
        }

        if (includes != null)
        {
            foreach (var includeExpression in includes)
            {
                query = query.Include(includeExpression);
            }
        }

        return query;
    }

    public IQueryable<TEntity> GetAllQueryable(params Expression<Func<TEntity, object>>[] includes)
    {
        return GetQueryable(null, includes);
    }

    public async Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await GetQueryable(where, includes).SingleOrDefaultAsync();

        return result;
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await GetQueryable(where, includes).FirstOrDefaultAsync();

        return result;
    }

    public async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {
        return await GetQueryable(where, includes).ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(expression);
    }

    public void Create(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public void CreateMany(List<TEntity> entities)
    {
        _dbContext.Set<TEntity>().AddRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void UpdateMany(List<TEntity> entities)
    {
        _dbContext.Set<TEntity>().UpdateRange(entities);
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

