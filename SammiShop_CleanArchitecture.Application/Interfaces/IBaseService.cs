using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IBaseService<TEntity> : IBaseReponsitory<TEntity>
        where TEntity : class
    {

    }
}
