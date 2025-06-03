namespace LicensePlate.Models.Authentication;

public sealed record LoginResponse(
    bool IsSuccess,
    Message[]? Messages,
    
    Guid? Id,
    string? Token,
    DateTime? Expires
);
