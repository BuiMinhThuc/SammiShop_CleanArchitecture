using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Domain.Entities;

namespace SammiShop_CleanArchitecture.Persistence.Payload.Mappers
{
    public static class RoleConverter
    {
        public static Role DTOToEntity(this RoleDTO roleDTO)
        {
            return new Role
            {
                Id = roleDTO.Id,
                KeyRole = roleDTO.KeyCode,

            };
        }
        public static RoleDTO EntityToDTO(this Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                KeyCode = role.KeyRole
            };
        }

    }
}
