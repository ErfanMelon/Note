using CSharpFunctionalExtensions;
namespace MyNoteApi.Repositories.Services;
public static class ExtentionMethods
{
    public static object ToResult(this Result result)
    {
        result.TryGetError(out var error);
        return new
        {
            result.IsFailure,
            result.IsSuccess,
            Error = error
        };
    }
    public static object ToResult<T>(this Result<T> result)
    {
        result.TryGetValue(out var value, out var error);
        return new
        {
            result.IsFailure,
            result.IsSuccess,
            Error = error,
            Value = value
        };
    }
    public static Guid ToGuid(this string guid)
    {
        var isGuid = Guid.TryParse(guid, out var result);
        return isGuid ? result : Guid.Empty;
    }
}
