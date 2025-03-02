using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.CardRequest;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class CardService : ICardService
    {
        private readonly IBaseService<Card> _baseCardService;
        private readonly IBaseService<Product> _baseProductService;

        public CardService(IBaseService<Card> baseCardService,
            IBaseService<Product> baseProductService)
        {
            _baseCardService = baseCardService;
            _baseProductService = baseProductService;
        }
        public async Task<CardDTO> AddAsync(Guid userId, CreateCardRequest request)
        {
            if (await InValid(request.ProductId, request.Quantity))
                return null;

            var card = await _baseCardService.GetAsync(x => x.ProductId == request.ProductId
                                             && x.UserId == userId);

            if (card != null)
            {
                card.Quantity += request.Quantity;
                card = await _baseCardService.UpdateAsync(card);
                return card.EntityToDTO();
            }
            else
            {
                var cardFromRequest = CreateCardFromRequest(userId, request);
                card = await _baseCardService.CreateAsync(cardFromRequest);
                return card.EntityToDTO();
            }
        }

        public async Task<CardDTO> DeleteByIdAsync(Guid cardId)
        {
            var card = await _baseCardService.GetByIdAsync(cardId);
            if (card == null)
                return null;

            await _baseCardService.DeleteAsync(card);
            return card.EntityToDTO();
        }

        public async Task<IQueryable<CardDTO>> GetAllAsync(PaginationExtension pagination)
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

        public async Task<CardDTO> UpdateAsync(UpdateCardRequest request)
        {
            var card = await _baseCardService.GetByIdAsync(request.Id);
            if (card == null)
                return null;


            if (await InValid(request.ProductId, request.Quantity))
                return null;

            card.Quantity = request.Quantity;
            card.ProductId = request.ProductId;

            var result = await _baseCardService.UpdateAsync(card);

            return result.EntityToDTO();

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

        private async Task<bool> InValid(Guid productId, int quantity)
            => await _baseProductService.GetAsync(x => x.Id == productId
                                                    && x.Quantity >= quantity)
                                                        is null;
        #endregion
    }
}
