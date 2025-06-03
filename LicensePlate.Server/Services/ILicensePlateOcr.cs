using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services;

public interface ILicensePlateOcr {
    Task<LicensePlateOcrResult> DetectLicensePlateAsync(string imageBase64);
}
