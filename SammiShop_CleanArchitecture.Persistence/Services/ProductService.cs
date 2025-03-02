using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductRequest;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService<Product> _baseService;

        public ProductService(IBaseService<Product> baseService
          )
        {
            _baseService = baseService;
        }

        public async Task<ProductDTO> CreateAsync(CreateProductRequest request)
        {
            var product = new Product
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
            var result = await _baseService.CreateAsync(product);
            return result.EntityToDTO();
        }

        public async Task<ProductDTO> DeleteByIdAsync(Guid id)
        {
            var product = await _baseService.GetByIdAsync(id);
            if (product == null)
                return null;

            var entity = _baseService.DeleteAsync(product);
            return entity.Result.EntityToDTO();
        }

        public async Task<IQueryable<ProductDTO>> GetAllAsync(PaginationExtension pagination)
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

        public async Task<ProductDTO> UpdateAsync(UpdateProductRequest request)
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

            var entity = await _baseService.UpdateAsync(product);
            return entity.EntityToDTO();

        }
    }
}
