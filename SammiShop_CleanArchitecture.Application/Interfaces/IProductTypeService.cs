using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductTypeRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IProductTypeService

    {
        Task<ProductTypeDTO> CreateAsync(CreateProductTypeRequest request);
        Task<ProductTypeDTO> UpdateAsync(UpdateProductTypeRequest request);
        Task<ProductTypeDTO> DeleteByIdAsync(Guid id);
        Task<IQueryable<ProductTypeDTO>> GetAllAsync(PaginationExtension pagination);
        Task<ProductTypeDTO> GetByIdAsync(Guid id);
    }
}
