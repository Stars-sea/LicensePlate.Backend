﻿namespace LicensePlate.Server.Settings;

public sealed class TencentOcrSettings {
    public const string Section = "TencentOcrSettings";
    
    public string AppId { get; set; } = string.Empty;
    
    public string AppKey { get; set; } = string.Empty;
}
