namespace FestivalTicketsApp.Shared;

public class Result<TValue>
{
    public readonly TValue? Value;
    
    public readonly Error? Error;

    public readonly bool IsSuccess;

    private Result(bool isSuccess, Error? error, TValue? value)
    {
        if ((isSuccess && error is not null) ||
            (!isSuccess && error is null))
            throw new ArgumentException("Invalid result creation", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public static Result<TValue> Success(TValue value) => new(true, null, value);
    
    public static Result<TValue> Failure(Error errorInstance) => new(false, errorInstance, default);
}