using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IProductService
    {
        Task<ResponseObject<ProductDTO>> CreateAsync(CreateProductRequest request);
        Task<ResponseObject<ProductDTO>> UpdateAsync(UpdateProductRequest request);
        Task<ResponseObject<ProductDTO>> DeleteByIdAsync(Guid id);
        Task<IEnumerable<ProductDTO>> GetAllAsync(PaginationExtension pagination);
        Task<ProductDTO> GetByIdAsync(Guid id);
    }
}
