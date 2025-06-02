namespace LicensePlate.Server.Services;

internal interface IDateTimeProvider {
    public DateTime UtcNow { get; }
}
