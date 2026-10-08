using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CondoHub.Domain.Interfaces.Services;

namespace CondoHub.Domain.Util;

public static class ResultLogger
{
    public static ILogService? Logger { get; set; }

    public static void LogFailure(string errorMessage, TypeErrorLogEnum errorType) =>
        Logger?.RegisterLog(errorMessage, errorType);

    public static void LogFailureApi(string errorMessage, TypeErrorLogEnum errorType, int statusCode) =>
        Logger?.RegisterLog($"{errorMessage} | StatusCode: {statusCode}", errorType);
}
