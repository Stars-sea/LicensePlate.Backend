using System.IdentityModel.Tokens.Jwt;
using LicensePlate.Models;
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

    private static readonly Message NotFoundUserFromTokenMsg
        = ("NotFoundUserFromToken", "Can not find user from token");

    private string? UsernameFromToken =>
        User.Claims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Name))?.Value;

    [NonAction]
    private ImageServiceResult<string> EncodeImage(byte[]? image)
        => image != null
            ? imageService.BinaryImg2Base64(image)
            : ImageServiceResult<string>.Fail(("NoAvatar", "Avatar is null"));

    [HttpGet("{username}")]
    public async Task<ActionResult<ProfileResponse>> GetAsync(string username) {
        if (await userManager.FindByNameAsync(username) is not { } profile)
            return NotFound(
                new ProfileResponse(
                    false,
                    [("UserNotFound", "No such user")],
                    username,
                    null
                )
            );

        var avatar = EncodeImage(profile.AvatarSource);
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
    public async Task<ActionResult<DetailedProfileResponse>> GetAsync() {
        string? username = UsernameFromToken;
        if (username == null || await userManager.FindByNameAsync(username) is not { } profile)
            return NotFound(
                new DetailedProfileResponse(
                    false,
                    [NotFoundUserFromTokenMsg],
                    null,
                    null,
                    null
                )
            );

        var avatar = EncodeImage(profile.AvatarSource);
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

    [HttpGet("{username}/avatar")]
    public async Task<ActionResult> GetUserAvatarAsync(string username) {
        UserProfile? profile = await userManager.FindByNameAsync(username);
        if (profile?.AvatarSource == null) return NotFound();

        return new FileContentResult(profile.AvatarSource, "image/jpeg") {
            FileDownloadName = $"avatar_{profile.Id}.jpg"
        };
    }

    [HttpGet("avatar")]
    [Authorize]
    public ActionResult GetAvatarAsync() {
        string? username = UsernameFromToken;
        if (username == null) return NotFound();

        return RedirectToAction("GetUserAvatar", new { username });
    }

    [HttpPost("avatar")]
    [Authorize]
    public async Task<ActionResult<ProfileAvatarChangeResponse>> PostAvatarAsync(
        [FromBody] ProfileAvatarChangeRequest request
    ) {
        if (UsernameFromToken == null || await userManager.FindByNameAsync(UsernameFromToken) is not { } profile)
            return NotFound(
                new ProfileAvatarChangeResponse(false, [NotFoundUserFromTokenMsg])
            );

        if (request.AvatarBase64 == null) {
            profile.AvatarSource = null;
        } else {
            var avatarSource = imageService.Base64ToBinaryImg(request.AvatarBase64);
            if (avatarSource.IsSuccess)
                profile.AvatarSource = avatarSource.Content;
            else
                return BadRequest(new ProfileAvatarChangeResponse(false, avatarSource.Errors));
        }
        IdentityResult result = await userManager.UpdateAsync(profile);
        if (!result.Succeeded)
            return BadRequest(
                new ProfileAvatarChangeResponse(
                    false,
                    result.Errors.Select(e => new Message(e.Code, e.Description)).ToArray()
                )
            );
        return Ok(new ProfileAvatarChangeResponse(true, null));
    }
}
