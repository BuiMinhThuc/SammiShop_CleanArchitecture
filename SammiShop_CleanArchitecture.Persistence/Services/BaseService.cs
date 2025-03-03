using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork;
using System.Linq.Expressions;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class
    {
        private readonly IUnitOfWork _uow;
        public BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                await _uow.GetGenericReponsitory<TEntity>().CreateAsync(entity);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return entity;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<IEnumerable<TEntity>> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var result = await _uow.GetGenericReponsitory<TEntity>().DeleteAsync(expression);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var result = await _uow.GetGenericReponsitory<TEntity>().DeleteAsync(entity);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<TEntity> DeleteByIdAsync(Guid id)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var result = await _uow.GetGenericReponsitory<TEntity>().DeleteByIdAsync(id);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(PaginationExtension pagination, Expression<Func<TEntity, bool>> expresion = null)
        {

            var result = expresion == null
                ? await _uow.GetGenericReponsitory<TEntity>().GetAllAsync(pagination)
                : await _uow.GetGenericReponsitory<TEntity>().GetAllAsync(pagination, expresion);
            await _uow.CommitTransactionAsync();
            return await PaginationService<TEntity>.Pagination(result, pagination);

        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {

            var result = expression == null
                ? await _uow.GetGenericReponsitory<TEntity>().GetAllAsync()
                : await _uow.GetGenericReponsitory<TEntity>().GetAllAsync(expression);

            return result;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {

            var result = await _uow.GetGenericReponsitory<TEntity>().GetAsync(expression);
            await _uow.CommitTransactionAsync();
            return result;

        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _uow.GetGenericReponsitory<TEntity>().GetByIdAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var result = await _uow.GetGenericReponsitory<TEntity>().UpdateAsync(entity);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }


    }
}
