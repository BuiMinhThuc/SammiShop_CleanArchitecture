using Microsoft.EntityFrameworkCore;
using SammiShop_CleanArchitecture.Application.Domain;
using SammiShop_CleanArchitecture.Application.Services.ProductService;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork;
using System.Linq.Expressions;

namespace SammiShop_CleanArchitecture.Application.Services.ProductTyoeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _uow;


        public ProductTypeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region CheckExist
        private Task<bool> CheckProductTypeExist(Expression<Func<ProductType, bool>> expression)
        {
            return _uow.GetGenericReponsitory<ProductType>().GetAllAsync(expression).ContinueWith(task => task.Result.Any());
        }
        #endregion

        public async Task<ProductType> CreateAsync(ProductType entitie)
        {

            await _uow.BeginTransactionAsync();
            try
            {
                await _uow.GetGenericReponsitory<ProductType>().CreateAsync(entitie);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return entitie;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductType>> CreateAsync(IEnumerable<ProductType> entities)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                await _uow.GetGenericReponsitory<ProductType>().CreateAsync(entities);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return entities;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw ex;
            }

        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                await _uow.GetGenericReponsitory<ProductType>().DeleteByIdAsync(id);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task DeleteAsync(Expression<Func<ProductType, bool>> expression)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                await _uow.GetGenericReponsitory<ProductType>().DeleteAsync(expression);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task<IQueryable<ProductType>> GetAllAsync(Expression<Func<ProductType, bool>> expression = null)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var result = expression == null
                    ? await _uow.GetGenericReponsitory<ProductType>().GetAllAsync()
                    : await _uow.GetGenericReponsitory<ProductType>().GetAllAsync(expression);

                await _uow.CommitTransactionAsync();
                return result;
            }
            catch (Exception)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<IQueryable<ProductType>> GetAllAsync(PageRequest pageRequest,
            Expression<Func<ProductType, bool>> expression = null
            )
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var result = expression == null
                    ? await _uow.GetGenericReponsitory<ProductType>().GetAllAsync()
                    : await _uow.GetGenericReponsitory<ProductType>().GetAllAsync(expression);

                await _uow.CommitTransactionAsync();
                return result.Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
                            .Take(pageRequest.PageSize)
                            .AsNoTracking();
            }
            catch (Exception)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }


        public async Task<ProductType> GetByIdAsync(Expression<Func<ProductType, bool>> expresion)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var result = await _uow.GetGenericReponsitory<ProductType>().GetByIdAsync(expresion);
                await _uow.CommitTransactionAsync();
                return result;
            }
            catch (Exception)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ProductType> UpdateByIdAsync(Guid id, ProductType entity)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var productType = await _uow.GetGenericReponsitory<ProductType>().UpdateByIdAsync(id, entity);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return productType;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductType>> UpdateAsync(IEnumerable<ProductType> entities)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var result = await _uow.GetGenericReponsitory<ProductType>().UpdateAsync(entities);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw ex;
            }
        }
    }
}
