namespace LicensePlate.Models.Detect;

public sealed record Rect(long? X, long? Y, long? Width, long? Height);

public sealed record PlateInfo(
    string Number,
    long? Confidence,
    Rect Rect,
    string Color
);
