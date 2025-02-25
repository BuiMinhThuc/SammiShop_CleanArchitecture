
using SammiShop_CleanArchitecture.API.Payload.DTOs;
using SammiShop_CleanArchitecture.Domain.Entities;

namespace SammiShop_CleanArchitecture.Application.API.Mappers
{
    public static class ProductTypeConverter
    {
        public static ProductType DTOtoEntity(ProductTypeDTO productTypeDTO)
        {
            return new ProductType
            {
                Id = productTypeDTO.Id,
                TypeName = productTypeDTO.TypeName,
            };
        }
        public static ProductTypeDTO EntitytoDTO(ProductType productType)
        {
            return new ProductTypeDTO
            {
                Id = productType.Id,
                TypeName = productType.TypeName,
            };
        }
    }
}
