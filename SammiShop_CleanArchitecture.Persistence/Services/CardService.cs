using Microsoft.AspNetCore.Http;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.CardRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Constants;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class CardService : ICardService
    {
        private readonly IBaseService<Card> _baseCardService;
        private readonly IBaseService<Product> _baseProductService;
        private readonly ResponseObject<CardDTO> _responseCard;

        public CardService(IBaseService<Card> baseCardService,
            IBaseService<Product> baseProductService,
            ResponseObject<CardDTO> responseCard)
        {
            _baseCardService = baseCardService;
            _baseProductService = baseProductService;
            _responseCard = responseCard;
        }
        public async Task<ResponseObject<CardDTO>> AddAsync(Guid userId, CreateCardRequest request)
        {
            var card = await _baseCardService.GetAsync(card => card.ProductId == request.ProductId
                                             && card.UserId == userId);
            if (card != null)
            {
                card.Quantity += request.Quantity;
                card = await _baseCardService.UpdateAsync(card);
            }
            else
            {
                var cardFromRequest = CreateCardFromRequest(userId, request);
                card = await _baseCardService.CreateAsync(cardFromRequest);
            }

            return _responseCard.Success(CardConstant.ADD_PRODUCT_IN_CARD_SUCCESS, card.EntityToDTO());
        }

        public async Task<ResponseObject<CardDTO>> DeleteByIdAsync(Guid cardId)
        {
            var card = await _baseCardService.GetByIdAsync(cardId);
            if (card == null)
                return _responseCard.Error(StatusCodes.Status400BadRequest, CardConstant.NOT_FOUND_CARD, null);

            await _baseCardService.DeleteAsync(card);
            return _responseCard.Success(CardConstant.DELETE_PRODUCT_IN_CARD_SUCCESS, card.EntityToDTO());
        }

        public async Task<IEnumerable<CardDTO>> GetAllAsync(PaginationExtension pagination)
        {
            var cards = await _baseCardService.GetAllAsync(pagination);
            return cards.Select(x => x.EntityToDTO());
        }

        public async Task<CardDTO> GetByIdAsync(Guid cardId)
        {
            var card = await _baseCardService.GetByIdAsync(cardId);
            if (card == null)
                return null;

            return card.EntityToDTO();
        }

        public async Task<IEnumerable<CardDTO>> GetByUserIdAsync(Guid userId)
        {
            var cards = await _baseCardService.GetAllAsync(x => x.UserId == userId);
            if (cards == null)
                return null;

            return cards.Select(x => x.EntityToDTO());
        }

        public async Task<ResponseObject<CardDTO>> UpdateAsync(UpdateCardRequest request)
        {
            var card = await SetCardFromRequest(request);
            if (card == null)
                return _responseCard.Error(StatusCodes.Status400BadRequest, CardConstant.NOT_FOUND_CARD, null);

            var result = await _baseCardService.UpdateAsync(card);

            return _responseCard.Success(CardConstant.UPDATE_PRODUCT_IN_CARD_SUCCESS, card.EntityToDTO());
        }

        #region SUPPORT
        private Card CreateCardFromRequest(Guid userId, CreateCardRequest request)
        {
            return new Card
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };
        }
        private async Task<Card> SetCardFromRequest(UpdateCardRequest request)
        {
            var card = await _baseCardService.GetByIdAsync(request.Id);
            if (card == null)
                return null;

            card.Quantity = request.Quantity;
            card.ProductId = request.ProductId;

            return card;
        }
        #endregion
    }
}
