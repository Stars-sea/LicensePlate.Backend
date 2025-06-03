using LicensePlate.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicensePlate.Server.Controllers;

[ApiController]
[Route("api/v0/info")]
public class ApiInfoController : ControllerBase {
    [HttpGet]
    public ActionResult<ApiVersionResponse> GetApiVersion() => Ok(
        new ApiVersionResponse(
            "LicensePlate.Server",
            GetType().Assembly.GetName().Version ?? new Version(0, 0, 1)
        )
    );
}
