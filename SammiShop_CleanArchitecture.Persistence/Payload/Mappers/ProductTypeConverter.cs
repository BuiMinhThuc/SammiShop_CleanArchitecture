using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Domain.Entities;


namespace SammiShop_CleanArchitecture.Application.API.Mappers
{
    public static class ProductTypeConverter
    {
        public static ProductType DTOtoEntity(this ProductTypeDTO productTypeDTO)
        {
            return new ProductType
            {
                Id = productTypeDTO.Id,
                TypeName = productTypeDTO.TypeName,
            };
        }
        public static ProductTypeDTO EntitytoDTO(this ProductType productType)
        {
            return new ProductTypeDTO
            {
                Id = productType.Id,
                TypeName = productType.TypeName,
            };
        }
    }
}
