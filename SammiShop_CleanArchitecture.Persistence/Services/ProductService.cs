using Microsoft.AspNetCore.Http;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Constants;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService<Product> _baseService;
        private readonly ResponseObject<ProductDTO> _responseObject;

        public ProductService(IBaseService<Product> baseService,
            ResponseObject<ProductDTO> responseObject
          )
        {
            _baseService = baseService;
            _responseObject = responseObject;
        }

        public async Task<ResponseObject<ProductDTO>> CreateAsync(CreateProductRequest request)
        {
            var product = NewProductFromRequest(request);

            var entity = await _baseService.CreateAsync(product);

            return _responseObject.Success(ProductConstant.CREATE_PRODUCT_SUCCESS, entity.EntityToDTO());

            #region SUPPORT
            Product NewProductFromRequest(CreateProductRequest request)
            {
                return new Product
                {
                    Id = Guid.NewGuid(),
                    NameProduct = request.NameProduct,
                    Description = request.Description,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    ProductTypeId = request.ProductTypeId,
                    TrademarkId = request.TrademarkId,
                    UrlImg = request.UrlImg
                };
            }
            #endregion
        }

        public async Task<ResponseObject<ProductDTO>> DeleteByIdAsync(Guid id)
        {
            var product = await _baseService.GetByIdAsync(id);
            if (product == null)
                return _responseObject.Error(StatusCodes.Status404NotFound, ProductConstant.NOT_FOUND_PRODUCT, null);

            var entity = await _baseService.DeleteAsync(product);

            return _responseObject.Success(ProductConstant.DELETE_PRODUCT_SUCCESS, entity.EntityToDTO());
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync(PaginationExtension pagination)
        {
            var products = await _baseService.GetAllAsync(pagination);
            if (products == null)
                return null;

            return products.Select(x => x.EntityToDTO());
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            var result = await _baseService.GetByIdAsync(id);
            if (result == null)
                return null;

            return result.EntityToDTO();
        }

        public async Task<ResponseObject<ProductDTO>> UpdateAsync(UpdateProductRequest request)
        {
            var product = await GetProductFromRequest(request);
            if (product == null)
                return _responseObject.Error(StatusCodes.Status404NotFound, ProductConstant.NOT_FOUND_PRODUCT, null);

            var entity = await _baseService.UpdateAsync(product);
            return _responseObject.Success(ProductConstant.UPDATE_PRODUCT_SUCCESS, entity.EntityToDTO());

            #region SUPPORT
            async Task<Product> GetProductFromRequest(UpdateProductRequest request)
            {
                var product = await _baseService.GetByIdAsync(request.Id);
                if (product == null)
                    return null;

                product.NameProduct = request.NameProduct;
                product.Description = request.Description;
                product.Price = request.Price;
                product.Quantity = request.Quantity;
                product.ProductTypeId = request.ProductTypeId;
                product.TrademarkId = request.TrademarkId;
                product.UrlImg = request.UrlImg;
                return product;
            }
            #endregion
        }
    }
}
