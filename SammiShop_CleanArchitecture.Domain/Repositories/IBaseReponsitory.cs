﻿using SammiShop_CleanArchitecture.Domain.Extensions;
using System.Linq.Expressions;

namespace SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories
{
    public interface IBaseReponsitory<TEntity>
         where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> DeleteByIdAsync(Guid id);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expresion);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<IEnumerable<TEntity>> GetAllAsync(PaginationExtension pagination, Expression<Func<TEntity, bool>> expresion = null);
    }
}
