using Microsoft.AspNetCore.Identity;

namespace LicensePlate.Server.Services.Results;

internal sealed record AuthenticationResult(bool IsSuccess, ErrorMessage[] Errors, JwtTokenGeneratorResult? Content)
    : IServiceResult {
    public static AuthenticationResult Succeed(JwtTokenGeneratorResult? token)
        => new(true, [], token);

    public static AuthenticationResult Fail(params IEnumerable<IdentityError> errors)
        => new(false, errors.Select(e => (ErrorMessage)e).ToArray(), null);
}
