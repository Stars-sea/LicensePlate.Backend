using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LicensePlate.Server.Services.Authentication;
using LicensePlate.Server.Services.Results;
using LicensePlate.Server.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LicensePlate.Server.Services.Impl;

internal class JwtTokenGenerator(
    IOptions<JwtSettings> settings,
    IDateTimeProvider dateTimeProvider
) : IJwtTokenGenerator {

    private JwtSettings JwtSettings => settings.Value;

    public JwtTokenGeneratorResult Generate(string userId, string email, string username) {
        SigningCredentials signingCredentials = new(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret!)),
            SecurityAlgorithms.HmacSha256
        );

        DateTime expires = dateTimeProvider.UtcNow.AddDays(7);
        Guid     id      = Guid.NewGuid();

        Claim[] claims = [
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.Name, username),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, id.ToString())
        ];

        JwtSecurityToken securityToken = new(
            JwtSettings.ValidIssuer,
            JwtSettings.ValidAudience,
            claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        return new JwtTokenGeneratorResult(
            id,
            username,
            email,
            new JwtSecurityTokenHandler().WriteToken(securityToken),
            expires
        );
    }
}
