using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Domain.Entities;

namespace SammiShop_CleanArchitecture.Persistence.Payload.Mappers
{
    public static class TrademarkConverter
    {
        public static TrademarkDTO EntityToDTO(this Trademark trademark)
        {
            return new TrademarkDTO
            {
                Id = trademark.Id,
                TrademarkName = trademark.TrademarkName,
                Address = trademark.Address
            };
        }
        public static Trademark DTOToEntity(this TrademarkDTO trademarkDTO)
        {
            return new Trademark
            {
                Id = trademarkDTO.Id,
                TrademarkName = trademarkDTO.TrademarkName,
                Address = trademarkDTO.Address

            };
        }
    }
}
