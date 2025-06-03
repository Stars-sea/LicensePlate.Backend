namespace LicensePlate.Models.Authentication;

public sealed record RegisterResponse(
    bool IsSuccess,
    string? Url,
    Message[]? Messages
);
