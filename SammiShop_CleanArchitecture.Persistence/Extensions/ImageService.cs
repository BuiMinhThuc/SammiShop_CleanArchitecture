using Microsoft.AspNetCore.Http;
using SammiShop_CleanArchitecture.Domain.Extensions;
using SammiShop_CleanArchitecture.Infrastructure.Cloudinary;

namespace SammiShop_CleanArchitecture.Persistence.Extensions
{
    public class ImageService : IImageService
    {
        private readonly ICloudinaryService _cloudinaryService;

        public ImageService(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async Task<string> ReplaceImg(string imgOld, IFormFile formFile)
        {
            var url = string.Empty;

            if (imgOld == null)
            {
                url = null;
            }
            else
            {
                if (!InputExtension.IsImage(formFile))
                    return null;

                url = await _cloudinaryService.ReplaceImageAsync(imgOld, formFile);
            }

            return url;
        }

        public async Task<string> UploadImg(IFormFile file)
        {
            var url = string.Empty;

            if (file == null)
            {
                url = null;
            }
            else
            {
                if (!InputExtension.IsImage(file))
                    return null;

                url = await _cloudinaryService.UploadImageAsync(file);
            }

            return url;
        }
    }
}

