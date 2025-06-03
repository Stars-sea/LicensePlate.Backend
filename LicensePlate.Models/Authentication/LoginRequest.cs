namespace LicensePlate.Models.Authentication;

public sealed record LoginRequest(
    string Email,
    string Password
);