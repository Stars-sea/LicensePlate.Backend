using LicensePlate.Models;

namespace LicensePlate.Server.Services.Results;

public sealed record JwtTokenGeneratorResult(
    Guid Id,
    string Username,
    string Email,
    string Token,
    DateTime Expires
) : IServiceResult {
    public bool IsSuccess => true;

    public Message[] Errors => [];
}
