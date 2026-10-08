using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CondoHub.Domain.Interfaces.Services;

namespace CondoHub.Domain.Util;

public sealed record Error(string Message, TypeErrorLogEnum Type = default, int? StatusCode = null);

public class Result<T>
{
    public T Value { get; set; }
    public bool IsSuccess { get; set; }
    public Error? Error { get; set; }

    public Result()
    {
        IsSuccess = true;
        Error = new Error(string.Empty);
    }

    public static Result<T> Success(T value) => new Result<T> { Value = value, IsSuccess = true, Error = new Error(string.Empty) };

    public static Result<T> Failure(string errorMessage, TypeErrorLogEnum errorType)
    {
        ResultLogger.LogFailure(errorMessage, errorType);
        return new Result<T> { IsSuccess = false, Error = new Error(errorMessage, errorType), Value = default! };
    }
    public static Result<T> Failure(string errorMessage)
    {
        ResultLogger.LogFailure(errorMessage, default);
        return new Result<T> { IsSuccess = false, Error = new Error(errorMessage), Value = default! };
    }
}

public class ResultList<T>
{
    public List<T> Value { get; set; }
    public bool IsSuccess { get; set; }
    public Error? Error { get; set; }

    public ResultList()
    {
        IsSuccess = true;
        Error = new Error(string.Empty);
    }

    public ResultList(T value)
    {
        Value = new List<T> { value };
        IsSuccess = true;
        Error = new Error(string.Empty);
    }

    public ResultList(List<T> value)
    {
        Value = value;
        IsSuccess = true;
        Error = new Error(string.Empty);
    }

    public static ResultList<T> Success(List<T> value) => new ResultList<T> { Value = value, IsSuccess = true, Error = new Error(string.Empty) };

    public static ResultList<T> Failure(string errorMessage)
    {
        ResultLogger.LogFailure(errorMessage, default);
        return new ResultList<T> { IsSuccess = false, Error = new Error(errorMessage), Value = new List<T>() };
    }

    public static ResultList<T> Failure(string errorMessage, TypeErrorLogEnum errorType)
    {
        ResultLogger.LogFailure(errorMessage, errorType);
        return new ResultList<T> { IsSuccess = false, Error = new Error(errorMessage, errorType), Value = new List<T>() };
    }
}

public class Result
{
    public bool IsSuccess { get; set; }
    public Error? Error { get; set; }

    public Result()
    {
        IsSuccess = true;
        Error = new Error(string.Empty);
    }

    public static Result Success() => new Result { IsSuccess = true, Error = new Error(string.Empty) };
    public static Result Failure(string errorMessage)
    {
        ResultLogger.LogFailure(errorMessage, default);
        return new Result { IsSuccess = false, Error = new Error(errorMessage) };
    }
    public static Result Failure(string errorMessage, TypeErrorLogEnum errorType)
    {
        ResultLogger.LogFailure(errorMessage, errorType);
        return new Result { IsSuccess = false, Error = new Error(errorMessage, errorType) };
    }
}

public class ApiResult<T> : Result<T>
{
    public int StatusCode { get; set; }

    public static ApiResult<T> Success(T value, int statusCode) => new ApiResult<T> { Value = value, IsSuccess = true, Error = new Error(string.Empty)  , StatusCode = statusCode };
    public static ApiResult<T> Failure(string errorMessage, int statusCode)
    {
        ResultLogger.LogFailureApi(errorMessage, default, statusCode);
        return new ApiResult<T> { IsSuccess = false, Error = new Error(errorMessage), Value = default!, StatusCode = statusCode };
    }
    public static ApiResult<T> Failure(string errorMessage, TypeErrorLogEnum errorType, int statusCode)
    {
        ResultLogger.LogFailureApi(errorMessage, errorType, statusCode);
        return new ApiResult<T> { IsSuccess = false, Error = new Error(errorMessage, errorType), Value = default!, StatusCode = statusCode };
    }
}

public class ApiResultList<T> : ResultList<T>
{
    public int StatusCode { get; set; }

    public static ApiResultList<T> Success(List<T> value, int statusCode) => new ApiResultList<T> { Value = value, IsSuccess = true, Error = new Error(string.Empty), StatusCode = statusCode };
    public static ApiResultList<T> Failure(string errorMessage, int statusCode)
    {
        ResultLogger.LogFailureApi(errorMessage, default, statusCode);
        return new ApiResultList<T> { IsSuccess = false, Error = new Error(errorMessage), Value = default!, StatusCode = statusCode };
    }
    public static ApiResultList<T> Failure(string errorMessage, TypeErrorLogEnum errorType, int statusCode)
    {
        ResultLogger.LogFailureApi(errorMessage, errorType, statusCode);
        return new ApiResultList<T> { IsSuccess = false, Error = new Error(errorMessage, errorType), Value = default!, StatusCode = statusCode };
    }
}