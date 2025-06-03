namespace LicensePlate.Models.Profile;

public sealed record DetailedProfileResponse(
    bool IsSuccess,
    Message[]? Messages,
    string? Username,
    string? Email,
    string? AvatarBase64
) : ProfileResponse(IsSuccess, Messages, Username, AvatarBase64);
