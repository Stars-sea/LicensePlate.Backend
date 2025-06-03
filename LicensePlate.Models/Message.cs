namespace LicensePlate.Models;

public record Message(string Code, string Description) {
    public static implicit operator Message((string, string) error) => new(error.Item1, error.Item2);
}
