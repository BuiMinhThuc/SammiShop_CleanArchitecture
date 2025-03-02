using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> CreateAsync(CreateProductRequest request);
        Task<ProductDTO> UpdateAsync(UpdateProductRequest request);
        Task<ProductDTO> DeleteByIdAsync(Guid id);
        Task<IQueryable<ProductDTO>> GetAllAsync(PaginationExtension pagination);
        Task<ProductDTO> GetByIdAsync(Guid id);
    }
}
