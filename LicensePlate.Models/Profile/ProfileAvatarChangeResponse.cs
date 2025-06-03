namespace LicensePlate.Models.Profile;

public record ProfileAvatarChangeResponse(
    bool IsSuccess,
    Message[]? Messages
);
