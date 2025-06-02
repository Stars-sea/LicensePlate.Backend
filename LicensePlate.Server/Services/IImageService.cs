using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services;

internal interface IImageService {
    ImageServiceResult<string> BinaryImg2Base64(byte[] image);
    
    ImageServiceResult<byte[]> Base64ToBinaryImg(string base64);
    
    bool IsImageValid(byte[] image);
}
