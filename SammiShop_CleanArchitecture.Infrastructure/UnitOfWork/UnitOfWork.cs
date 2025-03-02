using Microsoft.EntityFrameworkCore.Storage;
using SammiShop_CleanArchitecture.Infrastructure.Data;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories;


namespace SammiShop_CleanArchitecture.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext dbContext { get; }


        private IDbContextTransaction _transaction;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(AppDbContext _dbContext

            )
        {
            dbContext = _dbContext;

            _repositories = new Dictionary<Type, object>();

        }


        public async Task<int> SaveChangeAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool dispose = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.dispose)
            {

                if (disposing)
                {
                    dbContext.Dispose();
                }

                this.dispose = true;
            }
        }

        public IBaseReponsetory<TEntity> GetGenericReponsitory<TEntity>() where TEntity : class

        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IBaseReponsetory<TEntity>;
            }
            var repository = new BaseRepository<TEntity>(dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
