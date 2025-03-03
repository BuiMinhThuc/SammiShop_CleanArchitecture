using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.UserRequest;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Domain;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(RegisterRequest request);
        Task<UserDTO> DeleteByIdAsync(Guid id);
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<IQueryable<UserDTO>> GetAllAsync(PaginationExtension pagination);
        Task<UserDTO> UpdateByMember(UpdateUserByMemberRequest entity);
        Task<UserDTO> UpdateByAdmin(UpdateUserByAdminRequest entity);
        Task<Token> LoginAsync(LoginRequest request);
        Task<Token> RenewTokenAsync(Token request);
        Task<string> CheckOTP(string otp);

    }

}
