using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.CardRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface ICardService
    {
        Task<CardDTO> AddAsync(Guid userId, CreateCardRequest request);
        Task<CardDTO> UpdateAsync(UpdateCardRequest request);
        Task<CardDTO> DeleteByIdAsync(Guid cardId);
        Task<IQueryable<CardDTO>> GetAllAsync(PaginationExtension pagination);
        Task<CardDTO> GetByIdAsync(Guid cardId);
        Task<IEnumerable<CardDTO>> GetByUserIdAsync(Guid userId);
    }
}
