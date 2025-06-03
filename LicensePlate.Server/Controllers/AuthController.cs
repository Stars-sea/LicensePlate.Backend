using LicensePlate.Models.Authentication;
using LicensePlate.Server.Services.Authentication;
using LicensePlate.Server.Services.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlate.Server.Controllers;

[ApiController]
[Route("api/v0/auth")]
public class AuthController(
    IAuthenticationService authentication
) : ControllerBase {

    [HttpPost("register")]
    public async Task<IActionResult> PostRegisterAsync([FromBody] RegisterRequest request) {
        (string email, string username, string password) = request;
        AuthenticationResult result = await authentication.RegisterAsync(email, username, password);

        return result.IsSuccess
            ? Ok(new RegisterResponse(true, $"api/v0/profile/{username}", result.Errors))
            : Conflict(new RegisterResponse(false, null, result.Errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> PostLoginAsync([FromBody] LoginRequest request) {
        (string email, string password) = request;
        AuthenticationResult result = await authentication.LoginAsync(email, password);

        if (!result.IsSuccess)
            return Unauthorized(new LoginResponse(false, result.Errors, null, null, null));
        
        JwtTokenGeneratorResult token = result.Content!;
        return Ok(
            new LoginResponse(
                token.IsSuccess,
                token.Errors,
                token.Id,
                token.Token,
                token.Expires
            )
        );
    }
}
