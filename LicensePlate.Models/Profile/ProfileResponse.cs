namespace LicensePlate.Models.Profile;

public record ProfileResponse(
    bool IsSuccess,
    Message[]? Messages,
    string? Username,
    string? AvatarBase64
);
