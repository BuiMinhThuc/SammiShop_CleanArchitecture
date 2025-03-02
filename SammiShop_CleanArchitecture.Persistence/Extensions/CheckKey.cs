using Microsoft.EntityFrameworkCore;
using SammiShop_CleanArchitecture.Infrastructure.Data;
using System.Linq.Expressions;

namespace SammiShop_CleanArchitecture.Persistence.Extensions
{
    public class CheckKey<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public CheckKey(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expresion)
        {
            return await _dbContext.Set<T>().AnyAsync(expresion);
        }
    }

}
