using Microsoft.AspNetCore.Http;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.RoleRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Constants;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseService<Role> _baseService;
        private readonly ResponseObject<RoleDTO> _reponseRole;
        public RoleService(IBaseService<Role> baseService,
            ResponseObject<RoleDTO> reponseRole)
        {
            _baseService = baseService;
            _reponseRole = reponseRole;
        }

        public async Task<ResponseObject<RoleDTO>> CreateAsync(CreateRoleRequest request)
        {
            var role = new Role()
            {
                Id = Guid.NewGuid(),
                KeyRole = request.KeyCode,
            };

            var result = await _baseService.CreateAsync(role);
            return _reponseRole.Success(RoleConstant.CREATE_ROLE_SUCCESS, result.EntityToDTO());
        }

        public async Task<ResponseObject<RoleDTO>> UpdateAsync(UpdateRoleRequest request)
        {
            var role = await _baseService.GetByIdAsync(request.Id);
            if (role == null)
                return _reponseRole.Error(StatusCodes.Status404NotFound, RoleConstant.NOT_FOUND_ROLE, null);

            role.KeyRole = request.KeyCode;
            var result = await _baseService.UpdateAsync(role);
            return _reponseRole.Success(RoleConstant.UPDATE_ROLE_SUCCESS, result.EntityToDTO());
        }
        public async Task<ResponseObject<RoleDTO>> DeleteByIdAsync(Guid id)
        {
            var role = await _baseService.GetByIdAsync(id);
            if (role == null)
                return _reponseRole.Error(StatusCodes.Status404NotFound, RoleConstant.NOT_FOUND_ROLE, null);

            var result = await _baseService.DeleteAsync(role);
            return _reponseRole.Success(RoleConstant.DELETE_ROLE_SUCCESS, result.EntityToDTO());
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync(PaginationExtension pagination)
        {
            var result = await _baseService.GetAllAsync(pagination);
            if (result == null)
                return null;

            return result.Select(x => x.EntityToDTO());
        }

        public async Task<RoleDTO> GetByIdAsync(Guid id)
        {
            var result = await _baseService.GetByIdAsync(id);
            if (result == null)
                return null;

            return result.EntityToDTO();
        }

    }
}
