using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.TrademarkRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface ITrademarkService
    {
        Task<TrademarkDTO> CreateAsync(CreateTrademarkRequest request);
        Task<TrademarkDTO> UpdateAsync(UpdateTrademarkRequest request);
        Task<TrademarkDTO> DeleteByIdAsync(Guid id);
        Task<IQueryable<TrademarkDTO>> GetAllAsync(PaginationExtension pagination);
        Task<TrademarkDTO> GetByIdAsync(Guid id);

    }
}
