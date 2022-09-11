using System.Linq.Expressions;
using TodoMinimalApi.Common;

namespace TodoMinimalApi.DataAccess.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        void Update(TEntity entity);
    }
}