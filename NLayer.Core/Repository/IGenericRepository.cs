using System.Linq.Expressions;

namespace NLayer.Core.Repository
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> Where(Expression<Func<TEntity,bool>> expression);
        Task<bool> AnyAsync(Expression<Func<TEntity,bool>> expression);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity );
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
