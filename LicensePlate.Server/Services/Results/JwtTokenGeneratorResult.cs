namespace LicensePlate.Server.Services.Results;

internal sealed record JwtTokenGeneratorResult(
    Guid Id,
    string Username,
    string Email,
    string Token,
    DateTime Expires
) : IServiceResult {
    public bool IsSuccess => true;

    public ErrorMessage[] Errors => [];
}
