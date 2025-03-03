using Microsoft.EntityFrameworkCore;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Infrastructure.Data;
using System.Linq.Expressions;

namespace SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories
{
    public class BaseRepository<TEntity> : IBaseReponsitory<TEntity>
        where TEntity : class
    {

        public DbContext DbContext { get; set; }
        public BaseRepository(AppDbContext dbContext
         )
        {
            DbContext = dbContext;

        }
        public DbSet<TEntity> DbSet
        {
            get
            {

                return DbContext.Set<TEntity>();
            }
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }
        public async Task<IEnumerable<TEntity>> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entities = expression == null
                ? await DbSet.ToListAsync()
                : await DbSet.Where(expression).ToListAsync();

            if (entities.Count == 0)
            {
                return null;
            }
            DbSet.RemoveRange(entities);
            return entities;

        }
        public Task<IQueryable<TEntity>> GetAllAsync(PaginationExtension pagination, Expression<Func<TEntity, bool>> expression = null)
        {
            var query = expression == null
                ? DbSet.AsQueryable()
                : DbSet.Where(expression).AsQueryable();

            return Task.FromResult(query);
        }
        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            var query = expression == null
                ? DbSet.AsQueryable()
                : DbSet.Where(expression).AsQueryable();
            return Task.FromResult(query);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expresion)
        {
            return await DbSet.FirstOrDefaultAsync(expresion);
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.FromResult(entity);
        }

        public async Task<TEntity> DeleteByIdAsync(Guid id)
        {
            var tEntity = await DbSet.FindAsync(id);
            if (tEntity == null)
                return null;

            DbSet.Remove(tEntity);
            return tEntity;
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return Task.FromResult(entity);
        }
        public IQueryable<TEntity> Include<TProperty>(
                    IQueryable<TEntity> query,
                    Expression<Func<TEntity, TProperty>> navigationProperty)
        {
            return query.Include(navigationProperty);
        }
    }
}
