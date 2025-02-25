
using SammiShop_CleanArchitecture.API.Payload.DTOs;
using SammiShop_CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
