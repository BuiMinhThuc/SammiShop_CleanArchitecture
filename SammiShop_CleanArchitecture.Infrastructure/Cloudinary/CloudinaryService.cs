
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SammiShop_CleanArchitecture.Infrastructure.Cloudinary;
using System.Text.RegularExpressions;

public class CloudinaryService : ICloudinaryService, IDisposable
{
    private readonly CloudinaryConfig _cloudinaryConfiguration;
    private readonly Cloudinary _cloudinary;
    private readonly ILogger<CloudinaryService> _logger;
    private readonly Regex _publicIdRegex;
    private const string CloudinaryUrlPattern = @"image/upload/v\d+/(.+)\.\w+$";

    public CloudinaryService(CloudinaryConfig cloudinaryConfiguration,
        ILogger<CloudinaryService> logger = null)
    {
        _logger = logger;

        _cloudinaryConfiguration = cloudinaryConfiguration;

        var account = new Account(cloudinaryConfiguration.CloudName
            , cloudinaryConfiguration.APIKey
            , cloudinaryConfiguration.APISecret);

        _cloudinary = new Cloudinary(account);

        _publicIdRegex = new Regex(CloudinaryUrlPattern, RegexOptions.Compiled);
    }

    /// <summary>
    /// Xóa ảnh từ Cloudinary theo public ID
    /// </summary>
    /// <param name="publicId">Public ID của ảnh cần xóa</param>
    /// <returns>True nếu xóa thành công, False nếu thất bại</returns>
    public async Task<bool> DeleteImageAsync(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
        {
            _logger?.LogWarning("Attempted to delete image with null or empty public ID");
            return false;
        }

        try
        {
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            return deletionResult.Result == "ok";
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error deleting image with public ID: {PublicId}", publicId);
            return false;
        }
    }

    /// <summary>
    /// Tải ảnh lên Cloudinary từ IFormFile
    /// </summary>
    /// <param name="image">File ảnh cần tải lên</param>
    /// <param name="folder">Thư mục trên Cloudinary (tùy chọn)</param>
    /// <returns>URL bảo mật của ảnh đã tải lên hoặc null nếu thất bại</returns>
    public async Task<string> UploadImageAsync(IFormFile image, string folder = null)
    {
        if (image == null || image.Length == 0)
        {
            _logger?.LogWarning("Attempted to upload null or empty image");
            return null;
        }

        try
        {
            using var stream = image.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, stream),
                // Tùy chọn thêm cho tối ưu
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false
            };

            // Thêm folder nếu được chỉ định
            if (!string.IsNullOrEmpty(folder))
            {
                uploadParams.Folder = folder;
            }

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error uploading image: {FileName}", image.FileName);
            return null;
        }
    }

    /// <summary>
    /// Thay thế ảnh cũ bằng ảnh mới trên Cloudinary
    /// </summary>
    /// <param name="url">URL của ảnh cũ</param>
    /// <param name="newImage">File ảnh mới</param>
    /// <returns>URL bảo mật của ảnh mới hoặc null nếu thất bại</returns>
    public async Task<string> ReplaceImageAsync(string url, IFormFile newImage)
    {
        if (string.IsNullOrEmpty(url) || newImage == null || newImage.Length == 0)
        {
            _logger?.LogWarning("Invalid parameters for image replacement: URL={Url}, HasNewImage={HasNewImage}",
                url, newImage != null && newImage.Length > 0);
            return null;
        }

        try
        {
            var match = _publicIdRegex.Match(url);
            if (!match.Success)
            {
                _logger?.LogWarning("Could not extract public ID from URL: {Url}", url);
                return await UploadImageAsync(newImage);
            }

            var publicId = match.Groups[1].Value;

            var newImageUrl = await UploadImageAsync(newImage);
            if (string.IsNullOrEmpty(newImageUrl))
            {
                return null;
            }

            await DeleteImageAsync(publicId);

            return newImageUrl;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error replacing image: {Url}", url);
            return null;
        }
    }

    /// <summary>
    /// Trích xuất public ID từ URL Cloudinary
    /// </summary>
    /// <param name="url">URL Cloudinary</param>
    /// <returns>Public ID hoặc null nếu không tìm thấy</returns>
    public string ExtractPublicIdFromUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return null;
        }

        var match = _publicIdRegex.Match(url);
        return match.Success ? match.Groups[1].Value : null;
    }

    public void Dispose()
    {

        GC.SuppressFinalize(this);
    }
}


