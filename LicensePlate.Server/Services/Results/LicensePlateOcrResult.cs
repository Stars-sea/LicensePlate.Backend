using LicensePlate.Models;
using TencentCloud.Ocr.V20181119.Models;

namespace LicensePlate.Server.Services.Results;

public sealed record LicensePlateOcrResult(bool IsSuccess, Message[] Errors, LicensePlateInfo[] Infos) : IServiceResult {
    public static LicensePlateOcrResult Fail(params IEnumerable<Message> errors) 
        => new(false, errors.ToArray(), []);
    
    public static LicensePlateOcrResult Succeed(params IEnumerable<LicensePlateInfo> infos)
        => new(true, [], infos.ToArray());
}
