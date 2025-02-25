using Microsoft.EntityFrameworkCore;
using SammiShop_CleanArchitecture.Infrastructure.Data;
using System.Linq.Expressions;

namespace SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories
{
    public class BaseRepository<TEntity> : IBaseReponsetory<TEntity> where TEntity : class

    {

        public DbContext DbContext { get; set; }
        public BaseRepository(AppDbContext dbContext)
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
        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
            return entities;
        }
        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
            {
                return;
            }

            DbSet.Remove(entity);
            return;
        }
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entities = expression == null
                ? await DbSet.ToListAsync()
                : await DbSet.Where(expression).ToListAsync();

            if (entities.Count == 0)
            {
                return;
            }
            DbSet.RemoveRange(entities);
            return;

        }

        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            var query = expression == null
                ? DbSet.AsQueryable()
                : DbSet.Where(expression).AsQueryable();

            return Task.FromResult(query);
        }




        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expresion)
        {
            return await DbSet.FirstOrDefaultAsync(expresion);
        }



        public async Task<TEntity> UpdateByIdAsync(Guid id, TEntity entity)
        {
            DbSet.Update(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            return entities;
        }


    }

}
