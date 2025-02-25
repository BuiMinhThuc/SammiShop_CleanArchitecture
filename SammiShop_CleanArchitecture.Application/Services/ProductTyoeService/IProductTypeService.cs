using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories;

namespace SammiShop_CleanArchitecture.Application.Services.ProductService
{
    public interface IProductTypeService : IBaseReponsetory<ProductType>
    {
    }
}
