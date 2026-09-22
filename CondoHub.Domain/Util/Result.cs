using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CondoHub.Domain.Interfaces.Services;

namespace CondoHub.Domain.Util;

public class Result<T>
{
    public T Value { get; set; }
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }

    public Result()
    {
        IsSuccess = true;
        Error = string.Empty;
    }

    public static Result<T> Success(T value) => new Result<T> { Value = value, IsSuccess = true, Error = string.Empty };

    public static Result<T> Failure(string errorMessage, TypeErrorLogEnum errorType)
    {
        ResultLogger.LogFailure(errorMessage, errorType);
        return new Result<T> { IsSuccess = false, Error = errorMessage, Value = default! };
    }
    public static Result<T> Failure(string errorMessage)
    {
        ResultLogger.LogFailure(errorMessage, default);
        return new Result<T> { IsSuccess = false, Error = errorMessage, Value = default! };
    }
}

public class ResultList<T>
{
    public List<T> Value { get; set; }
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }

    public ResultList()
    {
        IsSuccess = true;
        Error = string.Empty;
    }

    public ResultList(T value)
    {
        Value = new List<T> { value };
        IsSuccess = true;
        Error = string.Empty;
    }

    public ResultList(List<T> value)
    {
        Value = value;
        IsSuccess = true;
        Error = string.Empty;
    }

    public static ResultList<T> Success(List<T> value) => new ResultList<T> { Value = value, IsSuccess = true, Error = string.Empty };

    public static ResultList<T> Failure(string errorMessage)
    {
        ResultLogger.LogFailure(errorMessage, default);
        return new ResultList<T> { IsSuccess = false, Error = errorMessage, Value = new List<T>() };
    }

    public static ResultList<T> Failure(string errorMessage, TypeErrorLogEnum errorType)
    {
        ResultLogger.LogFailure(errorMessage, errorType);
        return new ResultList<T> { IsSuccess = false, Error = errorMessage, Value = new List<T>() };
    }
}

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }

    public Result()
    {
        IsSuccess = true;
        Error = string.Empty;
    }

    public static Result Success() => new Result { IsSuccess = true, Error = string.Empty };
    public static Result Failure(string errorMessage)
    {
        ResultLogger.LogFailure(errorMessage, default);
        return new Result { IsSuccess = false, Error = errorMessage };
    }
    public static Result Failure(string errorMessage, TypeErrorLogEnum errorType)
    {
        ResultLogger.LogFailure(errorMessage, errorType);
        return new Result { IsSuccess = false, Error = errorMessage };
    }
}
