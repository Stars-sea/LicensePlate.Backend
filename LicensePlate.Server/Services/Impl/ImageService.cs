using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services.Impl;

internal class ImageService : IImageService {
    private const long MaxImgSize = 7 * 1024L * 1024L; // 7 MB

    public ImageServiceResult<string> BinaryImg2Base64(byte[] image)
        => IsImageValid(image)
            ? ImageServiceResult<string>.Succeed(Convert.ToBase64String(image))
            : ImageServiceResult<string>.Fail("Invalid image size (should < 7 MB)");

    public ImageServiceResult<byte[]> Base64ToBinaryImg(string base64) 
        => ImageServiceResult<byte[]>.Succeed(Convert.FromBase64String(base64));

    public bool IsImageValid(byte[] image) => image.LongLength < MaxImgSize;
}
