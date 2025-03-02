using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Domain.Entities;

namespace SammiShop_CleanArchitecture.Persistence.Payload.Mappers
{
    public static class CardConverter
    {
        public static CardDTO EntityToDTO(this Card card)
            => new CardDTO
            {
                Id = card.Id,
                ProductId = card.ProductId,
                UserId = card.UserId,
                Quantity = card.Quantity,
            };
    }
}
