namespace LicensePlate.Server.Services;

public interface IDateTimeProvider {
    public DateTime UtcNow { get; }
}
