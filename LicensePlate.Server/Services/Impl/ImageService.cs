using LicensePlate.Models;
using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services.Impl;

internal class ImageService : IImageService {

    private static readonly Message InvalidImgSizeMsg =
        ("InvalidImgSize", "Invalid image size (should < 7 MB).");

    private static readonly string[] SupportedTypes = [
        "image/jpeg",
        "image/png",
    ];
    
    public ImageServiceResult<string> BinaryImg2Base64(byte[] image)
        => IsImageValid(image)
            ? ImageServiceResult<string>.Succeed(Convert.ToBase64String(image))
            : ImageServiceResult<string>.Fail(InvalidImgSizeMsg);

    public ImageServiceResult<byte[]> Base64ToBinaryImg(string base64) {
        byte[] image = Convert.FromBase64String(base64);
        return IsImageValid(image)
            ? ImageServiceResult<byte[]>.Succeed(image)
            : ImageServiceResult<byte[]>.Fail(InvalidImgSizeMsg);
    }

    public async Task<ImageServiceResult<string>> FormFileToBase64Async(IFormFile file) {
        if (file.Length > IImageService.MaxImgSize)
            return ImageServiceResult<string>.Fail(InvalidImgSizeMsg);
        
        if (!SupportedTypes.Any(t => t.Equals(file.ContentType)))
            return ImageServiceResult<string>.Fail(("InvalidType", "Unsupported file type."));
        
        await using Stream stream = file.OpenReadStream();
        var buffer = new byte[file.Length];
        await stream.ReadExactlyAsync(buffer);
        
        return BinaryImg2Base64(buffer);
    }

    public bool IsImageValid(byte[] image) => image.LongLength < IImageService.MaxImgSize;

    public bool IsImageValid(string base64) => IsImageValid(Convert.FromBase64String(base64));
}
