namespace LicensePlate.Server.Services.Results;

internal interface IServiceResult {
    public bool IsSuccess { get; }
    
    public string[] Errors { get; }
}
