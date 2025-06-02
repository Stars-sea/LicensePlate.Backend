using LicensePlate.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace LicensePlate.Server;

public static class DependencyInjections {
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ApplicationDb>(options => {
            string connectionString = configuration.GetConnectionString("APPLICATION_DATABASE")!;
            options.UseMySQL(connectionString);
        });
    
    
}
