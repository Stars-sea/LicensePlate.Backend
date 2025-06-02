using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services.Authentication;

internal interface IJwtTokenGenerator {
    JwtTokenGeneratorResult Generate(string userId, string email, string username);
}
