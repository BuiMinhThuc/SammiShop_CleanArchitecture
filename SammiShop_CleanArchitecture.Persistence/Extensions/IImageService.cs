using Microsoft.AspNetCore.Http;

namespace SammiShop_CleanArchitecture.Persistence.Extensions
{
    public interface IImageService
    {
        Task<string> UploadImg(IFormFile file);
        Task<string> ReplaceImg(string imgOld, IFormFile formFile);

    }
}
