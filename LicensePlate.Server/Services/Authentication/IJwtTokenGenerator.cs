using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services.Authentication;

public interface IJwtTokenGenerator {
    JwtTokenGeneratorResult Generate(string userId, string email, string username);
}
