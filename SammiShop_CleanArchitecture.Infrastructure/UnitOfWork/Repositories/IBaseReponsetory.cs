using System.Linq.Expressions;

namespace SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories
{
    public interface IBaseReponsetory<TEntity> where TEntity : class
    {

        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateByIdAsync(Guid id, TEntity entity);
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);
        Task DeleteByIdAsync(Guid id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expresion);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expresion = null);
    }
}
