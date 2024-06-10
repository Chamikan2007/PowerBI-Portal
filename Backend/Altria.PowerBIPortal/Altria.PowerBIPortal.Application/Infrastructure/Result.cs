using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Application.Infrastructure;

public class Result
{
    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public Error? Error { get; }

    public static Result Success() => new Result(true, null);

    public static Result Faliour(Error error) => new Result(false, error);
}

public class Result<T> : Result
{
    protected Result(bool isSuccess, T? data, Error? error) : base(isSuccess, error)
    {
        Data = data;
    }

    public T? Data { get; }

    public static Result<T> Success(T data) => new Result<T>(true, data, null);

    public static new Result<T> Faliour(Error error) => new Result<T>(false, default, error);
}
