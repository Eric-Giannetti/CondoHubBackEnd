using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public static Result<T> Failure(string errorMessage) => new Result<T>
    { IsSuccess = false, Error = errorMessage, Value = default! };
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
    public static ResultList<T> Failure(string errorMessage) => new ResultList<T>
    { IsSuccess = false, Error = errorMessage, Value = new List<T>() };
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
    public static Result Failure(string errorMessage) => new Result { IsSuccess = false, Error = errorMessage };
}
