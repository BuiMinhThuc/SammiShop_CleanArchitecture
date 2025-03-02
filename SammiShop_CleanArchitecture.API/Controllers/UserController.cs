
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SammiShop_CleanArchitecture.API.Constants;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.Requests.UserRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Domain;

namespace SammiShop_CleanArchitecture.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]

        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationExtension pagination)
        {
            var result = await _userService.GetAllAsync(pagination);
            if (result == null || !result.Any())
                return NotFound(UserConstant.LIST_USER_NULL);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUserByIdAsync")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = $"{RoleConstant.ROLE_ADMIN},{RoleConstant.ROLE_MEMBER}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result == null)
                return NotFound(UserConstant.NOT_FOUND_USER);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RegisterRequest request)
        {
            var createdUser = await _userService.CreateAsync(request);
            if (createdUser == null)
                return BadRequest(UserConstant.CREATE_USER_FAIL);

            return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = createdUser.Id }, createdUser);
        }
        [HttpPut("users/member/{id}")]
        public async Task<IActionResult> UpdateByMemberAsync(UpdateUserByMemberRequest request)
        {
            if (await _userService.GetByIdAsync(request.Id) == null)
                return NotFound(UserConstant.NOT_FOUND_USER);

            await _userService.UpdateByMember(request);
            return Ok(UserConstant.UPDATE_USER_SUCCESS);
        }

        [HttpPut("admin/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = RoleConstant.ROLE_ADMIN)]
        public async Task<IActionResult> UpdateByAdminAsync(UpdateUserByAdminRequest request)
        {
            if (await _userService.GetByIdAsync(request.Id) == null)
                return NotFound(UserConstant.NOT_FOUND_USER);

            await _userService.UpdateByAdmin(request);
            return Ok(UserConstant.UPDATE_USER_SUCCESS);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);
            if (result == null)
                return BadRequest(UserConstant.LOGIN_FAIL);

            return Ok(result);
        }

        [HttpGet("renew-Token")]
        public async Task<IActionResult> RenewTokenAsync(Token refreshToken)
        {
            var result = await _userService.RenewTokenAsync(refreshToken);
            return Ok(result);
        }

        [HttpPut("ActivateAccount")]
        public async Task<IActionResult> ActiceAccount(string otp)
        {
            return Ok(await _userService.CheckOTP(otp));
        }
    }
}
