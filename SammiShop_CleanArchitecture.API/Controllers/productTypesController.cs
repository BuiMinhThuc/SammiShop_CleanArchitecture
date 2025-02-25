using Microsoft.AspNetCore.Mvc;
using SammiShop_CleanArchitecture.API.Payload.DTOs;
using SammiShop_CleanArchitecture.API.Payload.Requests;
using SammiShop_CleanArchitecture.Application.API.Mappers;
using SammiShop_CleanArchitecture.Application.Domain;
using SammiShop_CleanArchitecture.Application.Payload.Response;
using SammiShop_CleanArchitecture.Application.Services.ProductService;
using SammiShop_CleanArchitecture.Domain.Entities;

namespace SammiShop_CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productTypesController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;
        private readonly ResponseObject<ProductTypeDTO> _responseObject;


        public productTypesController(IProductTypeService productTypeService,
             ResponseObject<ProductTypeDTO> responseObject)
        {
            _productTypeService = productTypeService;
            _responseObject = responseObject;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PageRequest pageRequest)
        {

            var result = await _productTypeService.GetAllAsync();

            if (result == null || !result.Any())
            {
                return NotFound(_responseObject.Error(StatusCodes.Status404NotFound, "Không có loại sản phẩm nào!", null));
            }

            return Ok(result.Select(x => ProductTypeConverter.EntitytoDTO(x)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {

            var result = await _productTypeService.GetByIdAsync(p => p.Id == id);

            if (result == null)
            {
                return NotFound(_responseObject.Error(StatusCodes.Status404NotFound, "Không có loại sản phẩm này!", null));
            }

            return Ok(ProductTypeConverter.EntitytoDTO(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductTypeRequest productTypeRequest)
        {
            var productType = new ProductType
            {
                Id = Guid.NewGuid(),
                TypeName = productTypeRequest.TypeName
            };

            var result = await _productTypeService.CreateAsync(productType);

            return Ok(_responseObject.Success("Thêm loại sản phẩm mới thành công !", ProductTypeConverter.EntitytoDTO(result)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(Guid id, CreateProductTypeRequest productTypeRequest)
        {
            if (!await IsExistById(id))
            {
                return NotFound(_responseObject.Error(StatusCodes.Status404NotFound, "Không có loại sản phẩm này!", null));
            }
            var productType = new ProductType()
            {
                Id = id,
                TypeName = productTypeRequest.TypeName
            };
            productType.TypeName = productTypeRequest.TypeName;
            var result = await _productTypeService.UpdateByIdAsync(id, productType);
            return Ok(_responseObject.Success("Cập nhật loại sản phẩm thành công !", ProductTypeConverter.EntitytoDTO(result)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            if (!await IsExistById(id))
            {
                return NotFound(_responseObject.Error(StatusCodes.Status404NotFound, "Không có loại sản phẩm này!", null));
            }
            await _productTypeService.DeleteByIdAsync(id);

            return Ok(_responseObject.Success("Xóa loại sản phẩm thành công !", null));
        }




        #region CheckExist
        private async Task<bool> IsExistById(Guid id)
        {
            var productType = await _productTypeService.GetByIdAsync(p => p.Id == id);
            if (productType == null)
            {
                return false;
            }
            return true;
        }
        #endregion

    }
}
