using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.RoleRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDTO> CreateAsync(CreateRoleRequest request);
        Task<RoleDTO> UpdateAsync(UpdateRoleRequest request);
        Task<RoleDTO> DeleteByIdAsync(Guid id);
        Task<IQueryable<RoleDTO>> GetAllAsync(PaginationExtension pagination);
        Task<RoleDTO> GetByIdAsync(Guid id);
    }
}
