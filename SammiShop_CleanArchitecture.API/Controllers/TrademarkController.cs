using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SammiShop_CleanArchitecture.API.Constants;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.Requests.TrademarkRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.API.Controllers
{
    [Route("api/trademarks")]
    [ApiController]
    public class TrademarkController : ControllerBase
    {
        private readonly ITrademarkService _trademarkService;
        public TrademarkController(ITrademarkService trademarkService)
        {
            _trademarkService = trademarkService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationExtension pagination)
        {
            var entities = await _trademarkService.GetAllAsync(pagination);
            if (entities == null || !entities.Any())
                return NotFound(TrademarkConstant.LIST_TRADEMARK_NULL);

            return Ok(entities);
        }
        [HttpGet("{id}", Name = "GetTrademarkByIdAsync")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> GetTrademarkByIdAsync(Guid id)
        {
            var result = await _trademarkService.GetByIdAsync(id);
            if (result == null)
                return NotFound(TrademarkConstant.NOT_FOUND_TRADEMARK);

            return Ok(result);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> CreateAsync(CreateTrademarkRequest trademarkRequest)
        {
            var result = await _trademarkService.CreateAsync(trademarkRequest);
            if (result == null)
                return BadRequest(TrademarkConstant.CREATE_TRADEMARK_FAIL);

            return CreatedAtRoute(nameof(GetTrademarkByIdAsync), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> UpdateByIdAsync(UpdateTrademarkRequest entityRequest)
        {
            var result = await _trademarkService.UpdateAsync(entityRequest);
            if (result == null)
                return NotFound(TrademarkConstant.NOT_FOUND_TRADEMARK);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await _trademarkService.DeleteByIdAsync(id);
            if (result == null)
                return NotFound(TrademarkConstant.NOT_FOUND_TRADEMARK);

            return NoContent();
        }



    }
}
