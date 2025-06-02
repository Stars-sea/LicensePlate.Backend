using LicensePlate.Server.Database;
using LicensePlate.Server.Services;
using LicensePlate.Server.Services.Impl;
using LicensePlate.Server.Settings;
using Microsoft.EntityFrameworkCore;

namespace LicensePlate.Server;

internal static class DependencyInjections {
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ApplicationDb>(options => {
            string connectionString = configuration.GetConnectionString("APPLICATION_DATABASE")!;
            options.UseMySQL(connectionString);
        });

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<TencentOcrSettings>(configuration.GetSection("TencentOcr"));
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddSingleton<IImageService, ImageService>()
                   .AddSingleton<ILicensePlateOcr, LicensePlateOcr>();
}
