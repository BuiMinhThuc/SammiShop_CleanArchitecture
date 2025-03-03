using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.UserRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Persistence.Domain;

namespace SammiShop_CleanArchitecture.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseObject<UserDTO>> CreateAsync(RegisterRequest request);
        Task<ResponseObject<UserDTO>> DeleteByIdAsync(Guid id);
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllAsync(PaginationExtension pagination);
        Task<ResponseObject<UserDTO>> UpdateByMember(UpdateUserByMemberRequest entity);
        Task<ResponseObject<UserDTO>> UpdateByAdmin(UpdateUserByAdminRequest entity);
        Task<ResponseObject<Token>> LoginAsync(LoginRequest request);
        Task<ResponseObject<Token>> RenewTokenAsync(Token request);
        Task<string> CheckOTP(string otp);

    }

}
