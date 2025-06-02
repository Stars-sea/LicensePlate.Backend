using System.Text;
using LicensePlate.Server.Database;
using LicensePlate.Server.Models;
using LicensePlate.Server.Services;
using LicensePlate.Server.Services.Authentication;
using LicensePlate.Server.Services.Impl;
using LicensePlate.Server.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LicensePlate.Server;

internal static class DependencyInjections {
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ApplicationDb>(options => {
                string connectionString = configuration.GetConnectionString("APPLICATION_DATABASE")!;
                options.UseMySQL(connectionString);
            }
        );

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<TencentOcrSettings>(configuration.GetSection(TencentOcrSettings.Section))
                   .Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));
    
    public static IServiceCollection AddIdentity(this IServiceCollection services) {
        services.AddIdentity<UserProfile, IdentityRole>(options => {
                        options.User.RequireUniqueEmail         = true;
                        options.Password.RequireDigit           = true;
                        options.Password.RequiredLength         = 8;
                        options.Password.RequireNonAlphanumeric = false;
                    }
                )
                .AddEntityFrameworkStores<ApplicationDb>()
                .AddDefaultTokenProviders();
        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration) {
        JwtSettings settings = configuration.GetSection(JwtSettings.Section).Get<JwtSettings>()!;
        
        services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme             = JwtBearerDefaults.AuthenticationScheme;
            }
        ).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer           = true,
                ValidateAudience         = true,
                ValidateLifetime         = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer              = settings.ValidIssuer,
                ValidAudience            = settings.ValidAudience,
                IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret!))
            }
        );

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddOcrServices()
                   .AddSingleton<IDateTimeProvider, DateTimeProvider>();

    private static IServiceCollection AddOcrServices(this IServiceCollection services)
        => services.AddSingleton<IImageService, ImageService>()
                   .AddSingleton<ILicensePlateOcr, LicensePlateOcr>();
}
