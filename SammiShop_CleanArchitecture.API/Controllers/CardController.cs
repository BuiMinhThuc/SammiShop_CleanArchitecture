using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.Requests.CardRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.API.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            var result = await _cardService.GetByIdAsync(Id);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationExtension pagination)
        {
            var result = await _cardService.GetAllAsync(pagination);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet("getbyuserid")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByUserIdAsync()
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _cardService.GetByUserIdAsync(userId);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddAsync(CreateCardRequest request)
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _cardService.AddAsync(userId, request);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAsync(UpdateCardRequest request)
        {
            var result = await _cardService.UpdateAsync(request);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAsync(Guid cardId)
        {
            var result = await _cardService.DeleteByIdAsync(cardId);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
