namespace LicensePlate.Models;

public sealed record ApiVersionResponse(
    string AppName,
    Version Version
);
