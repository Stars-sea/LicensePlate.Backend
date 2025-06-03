using System.IdentityModel.Tokens.Jwt;
using LicensePlate.Models.Profile;
using LicensePlate.Server.Models;
using LicensePlate.Server.Services;
using LicensePlate.Server.Services.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlate.Server.Controllers;

[ApiController]
[Route("api/v0/profile")]
public class ProfileController(
    IImageService imageService,
    UserManager<UserProfile> userManager
) : ControllerBase {
    
    [NonAction]
    private NotFoundObjectResult NotFoundUser(string username)
        => NotFound(
            new ProfileResponse(
                false,
                [("UserNotFound", "No such user")],
                username,
                null
            )
        );
    
    [HttpGet("{username}")]
    public async Task<IActionResult> GetAsync(string username) {
        UserProfile? profile = await userManager.FindByNameAsync(username);
        if (profile == null) return NotFoundUser(username);

        var avatar = profile.AvatarSource != null
            ? imageService.BinaryImg2Base64(profile.AvatarSource)
            : ImageServiceResult<string>.Fail(("NoAvatar", "Avatar is null"));
        return Ok(
            new ProfileResponse(
                true,
                null,
                profile.UserName,
                avatar.Content
            )
        );
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAsync() {
        string? username = User.Claims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Name))?.Value;

        if (username == null)
            return NotFound(
                new DetailedProfileResponse(
                    false,
                    [("UserNotFound", "Can not get exist username from token")],
                    null,
                    null,
                    null
                )
            );
        
        UserProfile? profile = await userManager.FindByNameAsync(username);
        if (profile == null) return NotFoundUser(username);
        
        var avatar = profile.AvatarSource != null
            ? imageService.BinaryImg2Base64(profile.AvatarSource)
            : ImageServiceResult<string>.Fail(("NoAvatar", "Avatar is null"));
        return Ok(
            new DetailedProfileResponse(
                true,
                null,
                profile.UserName,
                profile.Email,
                avatar.Content
            )
        );
    }
}
