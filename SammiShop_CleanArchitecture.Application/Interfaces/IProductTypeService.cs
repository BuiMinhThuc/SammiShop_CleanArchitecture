using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductTypeRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IProductTypeService

    {
        Task<ResponseObject<ProductTypeDTO>> CreateAsync(CreateProductTypeRequest request);
        Task<ResponseObject<ProductTypeDTO>> UpdateAsync(UpdateProductTypeRequest request);
        Task<ResponseObject<ProductTypeDTO>> DeleteByIdAsync(Guid id);
        Task<IEnumerable<ProductTypeDTO>> GetAllAsync(PaginationExtension pagination);
        Task<ProductTypeDTO> GetByIdAsync(Guid id);
    }
}
