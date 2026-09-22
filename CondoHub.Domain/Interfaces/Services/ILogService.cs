using CondoHub.Domain.Util;

namespace CondoHub.Domain.Interfaces.Services;

public interface ILogService
{
    void RegisterLog(string message, TypeErrorLogEnum errorType);
}
