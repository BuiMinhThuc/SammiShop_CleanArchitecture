using Microsoft.EntityFrameworkCore;
using SammiShop_CleanArchitecture.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories
{
    public interface IBaseReponsetory<TEntity> where TEntity : class
    {
 
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateByIdAsync(Guid id,TEntity entity);
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);
        Task DeleteByIdAsync(Guid id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expresion);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> expresion=null);
    }
}
