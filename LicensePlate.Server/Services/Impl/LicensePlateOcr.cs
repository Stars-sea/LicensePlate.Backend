using LicensePlate.Server.Services.Results;
using LicensePlate.Server.Settings;
using Microsoft.Extensions.Options;
using TencentCloud.Common;
using TencentCloud.Ocr.V20181119;
using TencentCloud.Ocr.V20181119.Models;

namespace LicensePlate.Server.Services.Impl;

internal class LicensePlateOcr(IOptions<TencentOcrSettings> settings) : ILicensePlateOcr {

    private readonly Credential _credential = new() {
        SecretId  = settings.Value.AppId,
        SecretKey = settings.Value.AppKey
    };

    public async Task<LicensePlateOcrResult> DetectLicensePlateAsync(string imageBase64) {
        OcrClient client = new(_credential, "");

        LicensePlateOCRRequest request = new() { ImageBase64 = imageBase64 };
        try {
            LicensePlateOCRResponse response = await client.LicensePlateOCR(request);
            return LicensePlateOcrResult.Succeed(response.LicensePlateInfos);
        }
        catch (Exception e) {
            return LicensePlateOcrResult.Fail(e.Message);
        }
    }
}
