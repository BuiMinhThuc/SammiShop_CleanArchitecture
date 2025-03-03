using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.RoleRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Extensions;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseObject<RoleDTO>> CreateAsync(CreateRoleRequest request);
        Task<ResponseObject<RoleDTO>> UpdateAsync(UpdateRoleRequest request);
        Task<ResponseObject<RoleDTO>> DeleteByIdAsync(Guid id);
        Task<IEnumerable<RoleDTO>> GetAllAsync(PaginationExtension pagination);
        Task<RoleDTO> GetByIdAsync(Guid id);
    }
}
