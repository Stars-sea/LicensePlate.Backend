using LicensePlate.Models;
using Microsoft.AspNetCore.Identity;

namespace LicensePlate.Server.Services.Results;

public sealed record AuthenticationResult(bool IsSuccess, Message[] Errors, JwtTokenGeneratorResult? Content)
    : IServiceResult {
    public static AuthenticationResult Succeed(JwtTokenGeneratorResult? token)
        => new(true, [], token);

    public static AuthenticationResult Fail(params IEnumerable<IdentityError> errors)
        => new(false, errors.Select(e => new Message(e.Code, e.Description)).ToArray(), null);
}
