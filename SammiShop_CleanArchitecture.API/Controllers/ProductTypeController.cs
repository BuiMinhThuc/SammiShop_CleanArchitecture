using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SammiShop_CleanArchitecture.API.Constants;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductTypeRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;



namespace SammiShop_CleanArchitecture.API.Controllers
{
    [Route("api/producttypes")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService
             )
        {
            _productTypeService = productTypeService;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationExtension pageRequest)
        {

            var entities = await _productTypeService.GetAllAsync(pageRequest);
            if (entities == null || !entities.Any())
                return NotFound(ProductTypeConstant.LIST_PRODUCTTYPE_NULL);

            return Ok(entities);
        }

        [HttpGet("{id}", Name = "GetProductTypeByIdAsync")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> GetProductTypeByIdAsync(Guid id)
        {
            var result = await _productTypeService.GetByIdAsync(id);

            if (result == null)
                return NotFound(ProductTypeConstant.NOT_FOUND_PRODUCTTYPE);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> CreateAsync(CreateProductTypeRequest productTypeRequest)
        {
            var result = await _productTypeService.CreateAsync(productTypeRequest);
            if (result == null)
                return BadRequest(ProductTypeConstant.CREATE_PRODUCTTYPE_FAIL);

            return CreatedAtRoute(nameof(GetProductTypeByIdAsync), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> UpdateByIdAsync(UpdateProductTypeRequest productTypeRequest)
        {
            var result = await _productTypeService.UpdateAsync(productTypeRequest);
            if (result == null)
                return NotFound(ProductTypeConstant.NOT_FOUND_PRODUCTTYPE);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var result = await _productTypeService.DeleteByIdAsync(id);
            if (result is null)
                return NotFound(ProductTypeConstant.NOT_FOUND_PRODUCTTYPE);

            return NoContent();
        }



    }
}
