using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public static class PaginationService<TEntity>
    {
        public static async Task<IEnumerable<TEntity>> Pagination(IEnumerable<TEntity> query, PaginationExtension pageRequest)
        {
            if (pageRequest.PageSize == 0)
                return query;

            return query.Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize).Take(pageRequest.PageSize);
        }
    }
}
