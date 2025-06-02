using LicensePlate.Server.Services.Results;

namespace LicensePlate.Server.Services.Authentication;

internal interface IAuthenticationService {
    Task<AuthenticationResult> RegisterAsync(string email, string username, string password);

    Task<AuthenticationResult> LoginAsync(string email, string password);
}
