using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services;

public interface IImageService {
    public const long MaxImgSize = 7 * 1024L * 1024L; // 7 MB
    
    ImageServiceResult<string> BinaryImg2Base64(byte[] image);
    
    ImageServiceResult<byte[]> Base64ToBinaryImg(string base64);

    Task<ImageServiceResult<string>> FormFileToBase64Async(IFormFile file);
    
    bool IsImageValid(byte[] image);
    
    bool IsImageValid(string base64);
}
