using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services;

internal interface ILicensePlateOcr {
    Task<LicensePlateOcrResult> DetectLicensePlateAsync(string imageBase64);
}
