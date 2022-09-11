using System.Linq.Expressions;
using TodoMinimalApi.Common;
using TodoMinimalApi.Contexts;

namespace TodoMinimalApi.DataAccess.Repositories
{
    /// <summary>
    /// Generic repository class
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey> 
    {
        private readonly TodoContext _todoContext;
        public Repository(
            TodoContext todoContext
            )
        {
            _todoContext = todoContext;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            var dbSet = _todoContext.Set<TEntity>();

            return dbSet.Where(expression).AsQueryable();
        }

        public IQueryable<TEntity> GetAll()
        {
            var dbSet = _todoContext.Set<TEntity>();

            return dbSet.AsQueryable();
        }

        public void Insert(TEntity entity)
        {
            _todoContext.Add(entity);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _todoContext.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _todoContext.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity is IEntitySoftDelete entitySoftDelete)
            {
                entitySoftDelete.IsDeleted = true;
                _todoContext.Update(entitySoftDelete);
            }
            else
            {
                _todoContext.Remove(entity);
            }
            
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _todoContext.SaveChangesAsync(cancellationToken);
        }
    }
}
