using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Domain.Entities;

namespace SammiShop_CleanArchitecture.Persistence.Payload.Mappers
{
    public static class ProductConverter
    {
        public static ProductDTO EntityToDTO(this Product entity)
        {
            return new ProductDTO
            {
                Id = entity.Id,
                NameProduct = entity.NameProduct,
                Description = entity.Description,
                UrlImg = entity.UrlImg,
                Price = entity.Price,
                ProductTypeId = entity.ProductTypeId,
                Quantity = entity.Quantity,
                TrademarkId = entity.TrademarkId,
            };
        }
        public static Product DTOToEntity(this ProductDTO dto)
        {
            {
                return new Product
                {
                    Id = dto.Id,
                    NameProduct = dto.NameProduct,
                    Description = dto.Description,
                    UrlImg = dto.UrlImg,
                    Price = dto.Price,
                    ProductTypeId = dto.ProductTypeId,
                    Quantity = dto.Quantity,
                    TrademarkId = dto.TrademarkId,
                };
            }
        }
    }
}
