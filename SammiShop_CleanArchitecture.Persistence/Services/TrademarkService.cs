using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.TrademarkRequest;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class TrademarkService : ITrademarkService
    {
        private readonly IBaseService<Trademark> _baseService;
        public TrademarkService(IBaseService<Trademark> baseService)
        {
            _baseService = baseService;
        }

        public async Task<TrademarkDTO> CreateAsync(CreateTrademarkRequest request)
        {
            var tradeMark = new Trademark()
            {
                Id = Guid.NewGuid(),
                Address = request.Address,
                TrademarkName = request.TrademarkName
            };
            var result = await _baseService.CreateAsync(tradeMark);
            return result.EntityToDTO();
        }

        public async Task<TrademarkDTO> DeleteByIdAsync(Guid id)
        {
            var tradeMark = await _baseService.GetByIdAsync(id);
            if (tradeMark == null)
                return null;

            var result = _baseService.DeleteAsync(tradeMark);
            return result.Result.EntityToDTO();
        }

        public async Task<IQueryable<TrademarkDTO>> GetAllAsync(PaginationExtension pagination)
        {
            var result = await _baseService.GetAllAsync(pagination);
            if (result == null)
                return null;

            return result.Select(x => x.EntityToDTO());
        }

        public async Task<TrademarkDTO> GetByIdAsync(Guid id)
        {
            var result = await _baseService.GetByIdAsync(id);
            if (result == null)
                return null;
            return result.EntityToDTO();

        }

        public async Task<TrademarkDTO> UpdateAsync(UpdateTrademarkRequest request)
        {
            var result = await _baseService.GetByIdAsync(request.Id);
            if (result == null)
                return null;

            result.TrademarkName = request.TrademarkName;
            result.Address = request.Address;
            var updateResult = await _baseService.UpdateAsync(result);
            return updateResult.EntityToDTO();
        }
    }
}
