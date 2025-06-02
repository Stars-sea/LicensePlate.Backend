using TencentCloud.Ocr.V20181119.Models;

namespace LicensePlate.Server.Services.Results;

internal sealed record LicensePlateOcrResult(bool IsSuccess, ErrorMessage[] Errors, LicensePlateInfo[] Infos) : IServiceResult {
    public static LicensePlateOcrResult Fail(params IEnumerable<ErrorMessage> errors) 
        => new(false, errors.ToArray(), []);
    
    public static LicensePlateOcrResult Succeed(params IEnumerable<LicensePlateInfo> infos)
        => new(true, [], infos.ToArray());
}
