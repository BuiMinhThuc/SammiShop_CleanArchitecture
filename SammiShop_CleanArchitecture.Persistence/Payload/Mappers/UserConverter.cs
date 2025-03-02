using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Domain.Entities;

namespace SammiShop_CleanArchitecture.Persistence.Payload.Mappers
{
    public static class UserConverter
    {
        public static UserDTO EntityToDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                FullName = user.FullName,
                PassWord = user.PassWord,
                RoleId = user.RoleId,
                UrlAvt = user.UrlAvt
            };
        }
        public static User DTOToEntity(this UserDTO userDTO)
        {
            return new User
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                Address = userDTO.Address,
                FullName = userDTO.FullName,
                PassWord = userDTO.PassWord,
                RoleId = userDTO.RoleId,
                UrlAvt = userDTO.UrlAvt
            };
        }
    }
}
