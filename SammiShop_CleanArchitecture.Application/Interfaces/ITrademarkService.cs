using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.TrademarkRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface ITrademarkService
    {
        Task<ResponseObject<TrademarkDTO>> CreateAsync(CreateTrademarkRequest request);
        Task<ResponseObject<TrademarkDTO>> UpdateAsync(UpdateTrademarkRequest request);
        Task<ResponseObject<TrademarkDTO>> DeleteByIdAsync(Guid id);
        Task<IEnumerable<TrademarkDTO>> GetAllAsync(PaginationExtension pagination);
        Task<TrademarkDTO> GetByIdAsync(Guid id);

    }
}
