namespace LicensePlate.Server.Services.Results;

internal sealed record ImageServiceResult<T>(bool IsSuccess, string[] Errors, T? Content)
    : IServiceResult where T : notnull {
    public static ImageServiceResult<T> Succeed(T content)
        => new(true, [], content);

    public static ImageServiceResult<T> Fail(params IEnumerable<string> errors)
        => new(false, errors.ToArray(), default);
}
