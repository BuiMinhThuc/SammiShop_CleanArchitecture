using Microsoft.AspNetCore.Http;

namespace SammiShop_CleanArchitecture.Infrastructure.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<bool> DeleteImageAsync(string publicId);
        Task<string> UploadImageAsync(IFormFile image, string folder = null);
        Task<string> ReplaceImageAsync(string url, IFormFile newImage);
        string ExtractPublicIdFromUrl(string url);
    }
}
