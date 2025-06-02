using LicensePlate.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LicensePlate.Server.Database;

public class ApplicationDb(DbContextOptions options) : IdentityDbContext<UserProfile>(options) {
    
}
