using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.RoleRequest;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;

namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseService<Role> _baseService;
        public RoleService(IBaseService<Role> baseService)
        {
            _baseService = baseService;
        }

        public async Task<RoleDTO> CreateAsync(CreateRoleRequest request)
        {
            var role = new Role()
            {
                Id = Guid.NewGuid(),
                KeyRole = request.KeyCode,
            };

            var result = await _baseService.CreateAsync(role);
            return result.EntityToDTO();
        }

        public async Task<RoleDTO> DeleteByIdAsync(Guid id)
        {
            var role = await _baseService.GetByIdAsync(id);
            if (role == null)
                return null;

            var result = _baseService.DeleteAsync(role);
            return result.Result.EntityToDTO();
        }

        public async Task<IQueryable<RoleDTO>> GetAllAsync(PaginationExtension pagination)
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

        public async Task<RoleDTO> UpdateAsync(UpdateRoleRequest request)
        {
            var role = await _baseService.GetByIdAsync(request.Id);
            if (role == null)
                return null;

            role.KeyRole = request.KeyCode;
            var result = await _baseService.UpdateAsync(role);
            return result.EntityToDTO();
        }
    }
}
