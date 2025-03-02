using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SammiShop_CleanArchitecture.API.Constants;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.Requests.RoleRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationExtension pagination)
        {
            var roles = await _roleService.GetAllAsync(pagination);
            if (roles == null)
                return NotFound(RoleConstant.LIST_ROLE_NULL);

            return Ok(roles);
        }

        [HttpGet("{id}", Name = "GetRoleByIdAsync")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
                return NotFound(RoleConstant.NOT_FOUND_ROLE);

            return Ok(role);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> CreateAsync(CreateRoleRequest roleRequest)
        {
            var result = await _roleService.CreateAsync(roleRequest);
            if (result == null)
                return BadRequest(RoleConstant.CREATE_ROLE_FAIL);

            return CreatedAtRoute(nameof(GetRoleByIdAsync), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> UpdateAsync(UpdateRoleRequest roleRequest)
        {
            var result = await _roleService.UpdateAsync(roleRequest);
            if (result == null)
                return NotFound(RoleConstant.NOT_FOUND_ROLE);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _roleService.DeleteByIdAsync(id);
            if (result == null)
                return NotFound(RoleConstant.NOT_FOUND_ROLE);

            return NoContent();
        }
    }
}
