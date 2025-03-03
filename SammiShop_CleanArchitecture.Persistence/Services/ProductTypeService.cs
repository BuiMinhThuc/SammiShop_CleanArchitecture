using Microsoft.AspNetCore.Http;
using SammiShop_CleanArchitecture.Application.API.Mappers;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductTypeRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Constants;

namespace SammiShop_CleanArchitecture.Persistence.Services.ProductTyoeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IBaseService<ProductType> _baseService;
        private readonly ResponseObject<ProductTypeDTO> _responseProductType;

        public ProductTypeService(IBaseService<ProductType> baseService,
            ResponseObject<ProductTypeDTO> responseProductType)
        {
            _baseService = baseService;
            _responseProductType = responseProductType;
        }

        public async Task<ResponseObject<ProductTypeDTO>> CreateAsync(CreateProductTypeRequest request)
        {
            var productType = new ProductType()
            {
                Id = Guid.NewGuid(),
                TypeName = request.TypeName,
            };

            var result = await _baseService.CreateAsync(productType);

            return _responseProductType.Success(ProductTypeConstant.CREATE_PRODUCTTYPE_SUCCESS, result.EntitytoDTO());
        }

        public async Task<ResponseObject<ProductTypeDTO>> DeleteByIdAsync(Guid id)
        {
            var productType = await _baseService.GetByIdAsync(id);
            if (productType == null)
                return _responseProductType.Error(StatusCodes.Status404NotFound, ProductTypeConstant.DELETE_PRODUCTTYPE_SUCCESS, null);

            var result = await _baseService.DeleteAsync(productType);
            return _responseProductType.Success(ProductTypeConstant.DELETE_PRODUCTTYPE_SUCCESS, result.EntitytoDTO());
        }

        public async Task<IEnumerable<ProductTypeDTO>> GetAllAsync(PaginationExtension pagination)
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

        public async Task<ResponseObject<ProductTypeDTO>> UpdateAsync(UpdateProductTypeRequest request)
        {
            var productType = await _baseService.GetByIdAsync(request.Id);
            if (productType == null)
                return _responseProductType.Error(StatusCodes.Status404NotFound, ProductTypeConstant.DELETE_PRODUCTTYPE_SUCCESS, null);

            productType.TypeName = request.TypeName;
            var result = await _baseService.UpdateAsync(productType);
            return _responseProductType.Success(ProductTypeConstant.UPDATE_PRODUCTTYPE_SUCCESS, result.EntitytoDTO());
        }
    }
}
