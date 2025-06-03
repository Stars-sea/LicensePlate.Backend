using LicensePlate.Models;

namespace LicensePlate.Server.Services.Results;

public sealed record ImageServiceResult<T>(bool IsSuccess, Message[] Errors, T? Content)
    : IServiceResult where T : notnull {
    public static ImageServiceResult<T> Succeed(T content)
        => new(true, [], content);

    public static ImageServiceResult<T> Fail(params IEnumerable<Message> errors)
        => new(false, errors.ToArray(), default);
}
