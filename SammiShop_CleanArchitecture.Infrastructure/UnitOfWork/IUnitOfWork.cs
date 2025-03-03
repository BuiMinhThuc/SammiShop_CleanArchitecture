using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories;

namespace SammiShop_CleanArchitecture.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseReponsitory<TEntity> GetGenericReponsitory<TEntity>()
         where TEntity : class;

        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
