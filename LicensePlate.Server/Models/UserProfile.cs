using Microsoft.AspNetCore.Identity;

namespace LicensePlate.Server.Models;

public class UserProfile : IdentityUser {
    public byte[]? AvatarSource { get; set; }
}
