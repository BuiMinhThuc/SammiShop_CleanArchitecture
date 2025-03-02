using SammiShop_CleanArchitecture.Application.API.Mappers;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductTypeRequest;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Persistence.Services.ProductTyoeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IBaseService<ProductType> _baseService;

        public ProductTypeService(IBaseService<ProductType> baseService)
        {
            _baseService = baseService;
        }

        public async Task<ProductTypeDTO> CreateAsync(CreateProductTypeRequest request)
        {
            var productType = new ProductType()
            {
                Id = Guid.NewGuid(),
                TypeName = request.TypeName,
            };
            var result = await _baseService.CreateAsync(productType);
            return result.EntitytoDTO();
        }

        public async Task<ProductTypeDTO> DeleteByIdAsync(Guid id)
        {
            var productType = await _baseService.GetByIdAsync(id);
            if (productType == null)
                return null;

            var result = _baseService.DeleteAsync(productType);
            return result.Result.EntitytoDTO();
        }

        public async Task<IQueryable<ProductTypeDTO>> GetAllAsync(PaginationExtension pagination)
        {
            var result = await _baseService.GetAllAsync(pagination);
            return result.Select(x => x.EntitytoDTO());
        }

        public async Task<ProductTypeDTO> GetByIdAsync(Guid id)
        {
            var productType = await _baseService.GetByIdAsync(id);
            if (productType == null)
                return null;

            return productType.EntitytoDTO();
        }

        public async Task<ProductTypeDTO> UpdateAsync(UpdateProductTypeRequest request)
        {
            var productType = await _baseService.GetByIdAsync(request.Id);
            if (productType == null)
                return null;

            productType.TypeName = request.TypeName;
            var result = await _baseService.UpdateAsync(productType);
            return result.EntitytoDTO();
        }
    }
}
