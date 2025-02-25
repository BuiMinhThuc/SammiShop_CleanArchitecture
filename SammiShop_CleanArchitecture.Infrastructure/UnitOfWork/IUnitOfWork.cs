using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseReponsetory<TEntity> GetGenericReponsitory<TEntity>() where TEntity : class;
        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
