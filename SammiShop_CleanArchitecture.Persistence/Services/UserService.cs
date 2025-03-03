using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Requests.UserRequest;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Infrastructure.Cloudinary;
using SammiShop_CleanArchitecture.Infrastructure.Constant;
using SammiShop_CleanArchitecture.Infrastructure.EmailTo;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork;
using SammiShop_CleanArchitecture.Persistence.Constants;
using SammiShop_CleanArchitecture.Persistence.Domain;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
//v1
namespace SammiShop_CleanArchitecture.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IEmailService _emailService;
        private readonly IBaseService<User> _baseUserService;
        private readonly IBaseService<ConfirmEmail> _baseConfirmService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _uow;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ResponseObject<UserDTO> _reponseUser;
        private readonly ResponseObject<Token> _responseToken;
        public UserService(IBaseService<User> baseUserService,
            IConfiguration configuration,
            IUnitOfWork uow,
            IEmailService emailService,
            IBaseService<ConfirmEmail> baseConfirmService,
            ICloudinaryService cloudinaryService,
            ResponseObject<UserDTO> reponseUser,
            ResponseObject<Token> responseToken)
        {
            _baseUserService = baseUserService;
            _configuration = configuration;
            _uow = uow;
            _emailService = emailService;
            _baseConfirmService = baseConfirmService;
            _cloudinaryService = cloudinaryService;
            _responseToken = responseToken;
            _reponseUser = reponseUser;
        }

        public async Task<ResponseObject<UserDTO>> CreateAsync(RegisterRequest request)
        {

            if (!ValidateUserInput(request))
                return _reponseUser.Error(StatusCodes.Status400BadRequest, UserConstant.DATA_REQUEST_INVALID, null);

            if (await _uow.GetGenericReponsitory<User>().GetAsync(x => x.UserName.Equals(request.UserName)) != null)
                return _reponseUser.Error(StatusCodes.Status400BadRequest, UserConstant.USERNAME_ISEXIST, null);

            var newUser = await CreateUserFromRequest(request);
            await _uow.BeginTransactionAsync();

            try
            {
                var result = await _uow.GetGenericReponsitory<User>().CreateAsync(newUser);
                await _uow.SaveChangeAsync();

                var otp = await GenerateUniqueOTP();
                var confirmEmail = CreateConfirmEmailRequest(newUser.Id, otp, request.Email);

                var emailTo = new EmailTo
                {
                    Mail = request.Email,
                    Subject = ConstantPersistence.SUBJECT_EMAIL_REGISTER,
                    Content = ConstantPersistence.CONTENT_MAIL_REGISTER(otp),
                };

                if (await _emailService.SendEmailAsync(emailTo) != ConstantInfrastructure.SEND_MAIL_SUCCESS)
                {
                    await _uow.RollbackTransactionAsync();
                    return _reponseUser.Error(StatusCodes.Status400BadRequest, ConstantInfrastructure.SEND_MAIL_FAIL, null);
                }

                await _uow.GetGenericReponsitory<ConfirmEmail>().CreateAsync(confirmEmail);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();

                return _reponseUser.Success(UserConstant.CREATE_USER_SUCCESS, result.EntityToDTO());
            }
            catch (Exception)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }

            #region SUPPORT_REGISTER
            bool ValidateUserInput(RegisterRequest request)
            {
                return InputExtension.IsValidUsername(request.UserName)
                    && InputExtension.IsPassWord(request.PassWord)
                    && InputExtension.IsValiEmail(request.Email)
                    && InputExtension.IsValidPhoneNumber(request.PhoneNumber);
            }

            async Task<string> UploadImg(IFormFile formFile)
            {
                return formFile != null
                    ? await _cloudinaryService.UploadImageAsync(formFile)
                    : ConstantPersistence.LINK_AVATAR_DEFAULT;
            }

            async Task<User> CreateUserFromRequest(RegisterRequest request)
            {
                return new User
                {
                    Id = Guid.NewGuid(),
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    UrlAvt = await UploadImg(request.UrlAvt),
                    FullName = request.FullName,
                    Email = request.Email,
                    PassWord = BCrypt.Net.BCrypt.HashPassword(request.PassWord),
                    RoleId = Guid.Parse(ConstantPersistence.MEMBER_ID)
                };
            }

            ConfirmEmail CreateConfirmEmailRequest(Guid userId, string otp, string email)
            {
                return new ConfirmEmail
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    Otp = otp,
                    UserId = userId,
                    Expired = DateTime.Now.AddMinutes(ConstantPersistence.EXPIRED_TIME_OTP_EMAIL),
                    Status = false
                };
            }

            async Task<string> GenerateUniqueOTP()
            {
                string otp;
                ConfirmEmail existingOtp;

                do
                {
                    otp = await InputExtension.RandomOTP();
                    existingOtp = await _uow.GetGenericReponsitory<ConfirmEmail>().GetAsync(x => x.Otp == otp);
                } while (existingOtp != null);

                return otp;
            }
            #endregion
        }
        public async Task<ResponseObject<UserDTO>> DeleteByIdAsync(Guid id)
        {
            var entity = await _baseUserService.GetByIdAsync(id);
            if (entity == null)
                return _reponseUser.Error(StatusCodes.Status400BadRequest, UserConstant.NOT_FOUND_USER, null);

            return _reponseUser.Success(UserConstant.DELETE_USER_SUCCESS, entity.EntityToDTO());
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync(PaginationExtension pagination)
        {
            var users = await _baseUserService.GetAllAsync(pagination);
            return users.Select(x => x.EntityToDTO());
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            return (await _baseUserService.GetAsync(x => x.Id == id)).EntityToDTO();
        }

        public async Task<ResponseObject<UserDTO>> UpdateByAdmin(UpdateUserByAdminRequest request)
        {
            var user = await GetUserFromRequest(request);
            if (user == null)
                return _reponseUser.Error(StatusCodes.Status400BadRequest, UserConstant.NOT_FOUND_USER, null);

            var result = await _baseUserService.UpdateAsync(user);
            return _reponseUser.Success(UserConstant.UPDATE_USER_SUCCESS, result.EntityToDTO());

            #region SUPPORT
            async Task<User> GetUserFromRequest(UpdateUserByAdminRequest request)
            {
                var user = await _baseUserService.GetByIdAsync(request.Id);
                if (user == null)
                    return null;

                user.PassWord = BCrypt.Net.BCrypt.HashPassword(request.PassWord);
                user.PhoneNumber = request.PhoneNumber;
                user.Address = request.Address;
                user.UrlAvt = await GetUrlImgFromIFromfile(request.UrlAvt);
                user.FullName = request.FullName;
                user.Email = request.Email;
                user.RoleId = request.RoleId;
                user.UserName = request.UserName;

                return user;
            }
            #endregion
        }

        public async Task<ResponseObject<UserDTO>> UpdateByMember(UpdateUserByMemberRequest request)
        {
            var user = await GetUserFromRequest(request);
            if (user == null)
                return _reponseUser.Error(StatusCodes.Status400BadRequest, UserConstant.NOT_FOUND_USER, null);



            var result = await _baseUserService.UpdateAsync(user);
            return _reponseUser.Success(UserConstant.UPDATE_USER_SUCCESS, result.EntityToDTO());

            async Task<User> GetUserFromRequest(UpdateUserByMemberRequest request)
            {
                var user = await _baseUserService.GetByIdAsync(request.Id);
                if (user == null)
                    return null;

                user.PhoneNumber = request.PhoneNumber;
                user.Address = request.Address;
                user.UrlAvt = await GetUrlImgFromIFromfile(request.UrlAvt);
                user.FullName = request.FullName;
                user.Email = request.Email;
                user.RoleId = Guid.Parse(ConstantPersistence.MEMBER_ID);
                user.UserName = request.UserName;
                return user;
            }
        }

        public async Task<ResponseObject<Token>> RenewTokenAsync(Token request)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

                var tokenValidation = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value))
                };
                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.AccessToken, tokenValidation, out var validatedToken);
                if (validatedToken is not JwtSecurityToken jwtSecurityToken || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                    return _responseToken.Error(StatusCodes.Status400BadRequest, ConstantPersistence.INVALID_TOKEN, null);

                RefreshToken refreshToken = await _uow.GetGenericReponsitory<RefreshToken>().GetAsync(x => x.Token == request.RefreshToken);

                if (refreshToken == null)
                    return _responseToken.Error(StatusCodes.Status400BadRequest, ConstantPersistence.NOT_FOUND_TOKEN, null);

                if (refreshToken.Expired < DateTime.Now)
                    return _responseToken.Error(StatusCodes.Status400BadRequest, ConstantPersistence.TOKEN_NOT_EXPIRED, null);

                var user = await _uow.GetGenericReponsitory<User>().GetAsync(x => x.Id == refreshToken.UserId);
                if (user == null)
                    return _responseToken.Error(StatusCodes.Status400BadRequest, ConstantPersistence.USER_NOT_EXIST, null);

                var newToken = await GenerateAccessTokenAsync(user);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();

                var token = new Token
                {
                    AccessToken = newToken.AccessToken,
                    RefreshToken = newToken.RefreshToken
                };
                return _responseToken.Success(ConstantPersistence.GET_TOKEN_SUCCESS, token);
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<ResponseObject<Token>> LoginAsync(LoginRequest request)
        {
            if (ErrorRequestInput(request))
                return _responseToken.Error(StatusCodes.Status400BadRequest, UserConstant.DATA_REQUEST_INVALID, null);

            var user = await _baseUserService.GetAsync(x => x.UserName == request.UserName);

            if (user == null)
                return _responseToken.Error(StatusCodes.Status400BadRequest, UserConstant.NOT_FOUND_USER, null);

            var confirmEmail = await _baseConfirmService.GetAsync(x => x.UserId == user.Id);
            if (confirmEmail == null)
                return _responseToken.Error(StatusCodes.Status400BadRequest, UserConstant.LOGIN_FAIL, null);

            if (confirmEmail.Status == false)
                return _responseToken.Error(StatusCodes.Status400BadRequest, UserConstant.LOGIN_FAIL, null);

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PassWord))
                return _responseToken.Error(StatusCodes.Status400BadRequest, UserConstant.DATA_REQUEST_INVALID, null);

            return _responseToken.Success(ConstantPersistence.GET_TOKEN_SUCCESS, await GenerateAccessTokenAsync(user));

            bool ErrorRequestInput(LoginRequest request)
            {
                return string.IsNullOrEmpty(request.UserName)
                    || string.IsNullOrEmpty(request.Password);
            }
        }

        public async Task<string> CheckOTP(string otp)
        {
            var confirmEmail = await _uow.GetGenericReponsitory<ConfirmEmail>().GetAsync(x => x.Otp == otp);
            if (confirmEmail is null)
                return null;


            if (confirmEmail.Expired < DateTime.Now || confirmEmail.Status)
                return null;


            await _uow.BeginTransactionAsync();
            try
            {
                confirmEmail.Status = true;

                await _uow.GetGenericReponsitory<ConfirmEmail>().UpdateAsync(confirmEmail).ConfigureAwait(false);
                await _uow.SaveChangeAsync().ConfigureAwait(false);
                await _uow.CommitTransactionAsync().ConfigureAwait(false);

                return ConstantPersistence.ACTIVATE_USER_SUCCESS;
            }
            catch
            {
                await _uow.RollbackTransactionAsync().ConfigureAwait(false);
                return null;
            }
        }

        #region SUPPORT
        private async Task<Token> GenerateAccessTokenAsync(User user)
        {
            await _uow.BeginTransactionAsync();
            try
            {

                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

                var decentralization = await _uow.GetGenericReponsitory<Role>().GetByIdAsync(id: user.RoleId);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Username", user.UserName),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim(ClaimTypes.Role, decentralization?.KeyRole ?? "")
                }),
                    Expires = DateTime.UtcNow.AddHours(ConstantPersistence.EXPIRED_TIME_TOKEN),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = jwtTokenHandler.CreateToken(tokenDescription);
                var accessToken = jwtTokenHandler.WriteToken(token);
                var refreshToken = GenerateRefreshToken();

                RefreshToken rf = new RefreshToken
                {
                    Token = refreshToken,
                    Expired = DateTime.Now.AddHours(ConstantPersistence.EXPIRED_TIME_REFRESHTOKEN),
                    UserId = user.Id
                };

                _uow.GetGenericReponsitory<RefreshToken>().CreateAsync(rf);
                await _uow.SaveChangeAsync();
                await _uow.CommitTransactionAsync();

                Token tokenDTO = new Token
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                return tokenDTO;
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        private async Task<string> GetUrlImgFromIFromfile(IFormFile formFile)
        {
            var url = string.Empty;

            if (formFile is null)
                url = ConstantPersistence.LINK_AVATAR_DEFAULT;
            else
            {
                if (!InputExtension.IsImage(formFile))
                    return null;
                url = await _cloudinaryService.UploadImageAsync(formFile).ConfigureAwait(false);
            }

            return url;
        }
        #endregion
    }
}
