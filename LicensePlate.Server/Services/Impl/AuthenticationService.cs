using LicensePlate.Server.Models;
using LicensePlate.Server.Services.Authentication;
using LicensePlate.Server.Services.Results;
using Microsoft.AspNetCore.Identity;

namespace LicensePlate.Server.Services.Impl;

internal class AuthenticationService(
    UserManager<UserProfile> userManager,
    SignInManager<UserProfile> signInManager,
    IJwtTokenGenerator tokenGenerator
) : IAuthenticationService {

    public async Task<AuthenticationResult> RegisterAsync(string email, string username, string password) {
        IdentityResult result = await userManager.CreateAsync(
            new UserProfile {
                Email    = email,
                UserName = username
            },
            password
        );

        return result.Succeeded
            ? AuthenticationResult.Succeed(null)
            : AuthenticationResult.Fail(result.Errors.ToArray());
    }

    public async Task<AuthenticationResult> LoginAsync(string email, string password) {
        UserProfile? profile = await userManager.FindByEmailAsync(email);
        if (profile == null)
            return AuthenticationResult.Fail(
                new IdentityError {
                    Code        = "InvalidEmailOrPassword",
                    Description = "Invalid email or password."
                }
            );

        SignInResult result = await signInManager.CheckPasswordSignInAsync(profile, password, false);
        if (!result.Succeeded || profile.Email == null || profile.UserName == null)
            return AuthenticationResult.Fail(
                new IdentityError {
                    Code        = "InvalidEmailOrPassword",
                    Description = "Invalid email or password."
                }
            );

        return AuthenticationResult.Succeed(tokenGenerator.Generate(profile.Id, profile.Email, profile.UserName));
    }
}
