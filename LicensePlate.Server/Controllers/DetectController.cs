using LicensePlate.Models.Detect;
using LicensePlate.Server.Services;
using LicensePlate.Server.Services.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlate.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/v0/detect")]
public class DetectController(
    ILicensePlateOcr ocr,
    IImageService imageService
) : ControllerBase {

    [HttpPost]
    public async Task<ActionResult<DetectResponse>> DetectAsync([FromBody] DetectRequest request) {
        var decodeResult = imageService.Base64ToBinaryImg(request.ImageBase64);
        if (!decodeResult.IsSuccess)
            return BadRequest(
                new DetectResponse(
                    false,
                    decodeResult.Errors,
                    null
                )
            );

        LicensePlateOcrResult result   = await ocr.DetectLicensePlateAsync(request.ImageBase64);
        DetectResponse        response = new(result.IsSuccess, result.Errors, result.Infos);
        return result.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("image")]
    public async Task<ActionResult<DetectResponse>> DetectAsync(IFormFile file) {
        var encodeResult = await imageService.FormFileToBase64Async(file);
        if (!encodeResult.IsSuccess)
            return BadRequest(
                new DetectResponse(
                    false,
                    encodeResult.Errors,
                    null
                )
            );

        LicensePlateOcrResult result   = await ocr.DetectLicensePlateAsync(encodeResult.Content!);
        DetectResponse        response = new(result.IsSuccess, result.Errors, result.Infos);
        return result.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
