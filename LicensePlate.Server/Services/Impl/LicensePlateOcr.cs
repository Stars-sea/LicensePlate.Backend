using LicensePlate.Models.Detect;
using LicensePlate.Server.Services.Results;
using LicensePlate.Server.Settings;
using Microsoft.Extensions.Options;
using TencentCloud.Common;
using TencentCloud.Ocr.V20181119;
using TencentCloud.Ocr.V20181119.Models;
using Rect = LicensePlate.Models.Detect.Rect;
using TencentRect = TencentCloud.Ocr.V20181119.Models.Rect;

namespace LicensePlate.Server.Services.Impl;

internal class LicensePlateOcr(IOptions<TencentOcrSettings> settings) : ILicensePlateOcr {

    private readonly Credential _credential = new() {
        SecretId  = settings.Value.AppId,
        SecretKey = settings.Value.AppKey
    };

    private static Rect ToRect(TencentRect rect) => new(rect.X, rect.Y, rect.Width, rect.Height);

    private static PlateInfo ToInfo(LicensePlateInfo info) =>
        new(info.Number, info.Confidence, ToRect(info.Rect), info.Color);

    public async Task<LicensePlateOcrResult> DetectLicensePlateAsync(string imageBase64) {
        OcrClient client = new(_credential, "");

        LicensePlateOCRRequest request = new() { ImageBase64 = imageBase64 };
        try {
            LicensePlateOCRResponse response = await client.LicensePlateOCR(request);
            return LicensePlateOcrResult.Succeed(response.LicensePlateInfos.Select(ToInfo));
        }
        catch (Exception e) {
            return LicensePlateOcrResult.Fail((e.GetType().Name, e.Message));
        }
    }
}
