using Microsoft.AspNetCore.Http;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.TrademarkRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Constants;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class TrademarkService : ITrademarkService
    {
        private readonly IBaseService<Trademark> _baseService;
        private readonly ResponseObject<TrademarkDTO> _reponseTradeMark;
        public TrademarkService(IBaseService<Trademark> baseService,
            ResponseObject<TrademarkDTO> reponseTradeMark)
        {
            _baseService = baseService;
            _reponseTradeMark = reponseTradeMark;
        }

        public async Task<ResponseObject<TrademarkDTO>> CreateAsync(CreateTrademarkRequest request)
        {
            var tradeMark = new Trademark()
            {
                Id = Guid.NewGuid(),
                Address = request.Address,
                TrademarkName = request.TrademarkName
            };
            var result = await _baseService.CreateAsync(tradeMark);
            return _reponseTradeMark.Success(TrademarkConstant.CREATE_TRADEMARK_SUCCESS, result.EntityToDTO());
        }
        public async Task<ResponseObject<TrademarkDTO>> UpdateAsync(UpdateTrademarkRequest request)
        {
            var result = await _baseService.GetByIdAsync(request.Id);
            if (result == null)
                return _reponseTradeMark.Error(StatusCodes.Status404NotFound, TrademarkConstant.NOT_FOUND_TRADEMARK, null);

            result.TrademarkName = request.TrademarkName;
            result.Address = request.Address;
            var updateResult = await _baseService.UpdateAsync(result);
            return _reponseTradeMark.Success(TrademarkConstant.UPDATE_TRADEMARK_SUCCESS, result.EntityToDTO());
        }
        public async Task<ResponseObject<TrademarkDTO>> DeleteByIdAsync(Guid id)
        {
            var tradeMark = await _baseService.GetByIdAsync(id);
            if (tradeMark == null)
                return _reponseTradeMark.Error(StatusCodes.Status404NotFound, TrademarkConstant.NOT_FOUND_TRADEMARK, null);

            var entity = await _baseService.DeleteAsync(tradeMark);
            return _reponseTradeMark.Success(TrademarkConstant.DELETE_TRADEMARK_SUCCESS, entity.EntityToDTO());
        }

        public async Task<IEnumerable<TrademarkDTO>> GetAllAsync(PaginationExtension pagination)
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

    }
}
