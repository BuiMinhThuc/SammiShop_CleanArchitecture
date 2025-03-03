using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductTypeRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Constants;



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

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> UpdateByIdAsync(UpdateProductTypeRequest productTypeRequest)
        {
            var result = await _productTypeService.UpdateAsync(productTypeRequest);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var result = await _productTypeService.DeleteByIdAsync(id);

            return Ok(result);
        }



    }
}
