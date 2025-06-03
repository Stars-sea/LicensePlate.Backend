using LicensePlate.Models;
using LicensePlate.Models.Detect;

namespace LicensePlate.Server.Services.Results;

public sealed record LicensePlateOcrResult(bool IsSuccess, Message[] Errors, PlateInfo[] Infos) : IServiceResult {
    public static LicensePlateOcrResult Fail(params IEnumerable<Message> errors) 
        => new(false, errors.ToArray(), []);
    
    public static LicensePlateOcrResult Succeed(params IEnumerable<PlateInfo> infos)
        => new(true, [], infos.ToArray());
}
