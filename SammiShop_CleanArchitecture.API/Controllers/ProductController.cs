using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SammiShop_CleanArchitecture.API.Constants;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.Requests.ProductRequest;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Infrastructure.Data;
using SammiShop_CleanArchitecture.Persistence.Extensions;

namespace SammiShop_CleanArchitecture.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _dbContext;
        public ProductController(IProductService productService,
            AppDbContext dbContext)
        {
            _productService = productService;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationExtension pagination)
        {
            var result = await _productService.GetAllAsync(pagination);
            if (result == null || !result.Any())
                return NotFound(ProductConstant.NOT_FOUND_PRODUCT);

            return Ok(result);

        }
        [HttpGet("{id}", Name = "GetProductByIdAsync")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (result == null)
                return NotFound(ProductConstant.NOT_FOUND_PRODUCT);

            return Ok(result);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> CreateAsync(CreateProductRequest productRequest)
        {
            if (!await new CheckKey<ProductType>(_dbContext).IsExistsAsync(p => p.Id == productRequest.ProductTypeId))
                return BadRequest(ProductTypeConstant.NOT_FOUND_PRODUCTTYPE);

            if (!await new CheckKey<Trademark>(_dbContext).IsExistsAsync(p => p.Id == productRequest.TrademarkId))
                return BadRequest(TrademarkConstant.NOT_FOUND_TRADEMARK);

            var createdProduct = await _productService.CreateAsync(productRequest);
            return CreatedAtRoute(nameof(GetProductByIdAsync), new { id = createdProduct.Id }, createdProduct);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> UpdateAsync(UpdateProductRequest request)
        {
            var product = await _productService.UpdateAsync(request);
            if (product == null)
                return NotFound(ProductConstant.NOT_FOUND_PRODUCT);

            if (!await new CheckKey<ProductType>(_dbContext).IsExistsAsync(p => p.Id == request.ProductTypeId))
                return BadRequest(ProductTypeConstant.NOT_FOUND_PRODUCTTYPE);

            if (!await new CheckKey<Trademark>(_dbContext).IsExistsAsync(p => p.Id == request.TrademarkId))
                return BadRequest(TrademarkConstant.NOT_FOUND_TRADEMARK);

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _productService.DeleteByIdAsync(id);
            if (result == null)
                return NotFound(ProductConstant.NOT_FOUND_PRODUCT);

            return NoContent();
        }

    }
}
