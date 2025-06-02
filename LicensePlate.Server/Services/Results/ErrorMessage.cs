using Microsoft.AspNetCore.Identity;

namespace LicensePlate.Server.Services.Results;

internal record ErrorMessage(string Message, string Description) {
    public static implicit operator ErrorMessage((string, string) error) => new(error.Item1, error.Item2);

    public static implicit operator ErrorMessage(IdentityError error) => new(error.Code, error.Description);
}
