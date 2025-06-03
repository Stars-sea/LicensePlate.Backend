namespace LicensePlate.Models.Detect;

public sealed record DetectResponse(
    bool IsSuccess,
    Message[]? Messages,
    PlateInfo[]? Infos
);
