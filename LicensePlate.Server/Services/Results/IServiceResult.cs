using LicensePlate.Models;

namespace LicensePlate.Server.Services.Results;

public interface IServiceResult {
    public bool IsSuccess { get; }
    
    public Message[] Errors { get; }
}
