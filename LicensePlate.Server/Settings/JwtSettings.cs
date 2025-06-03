namespace LicensePlate.Server.Settings;

public sealed class JwtSettings {
    public const string Section = "JwtSettings";

    public string ValidIssuer { get; set; } = string.Empty;

    public string ValidAudience { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;
}
