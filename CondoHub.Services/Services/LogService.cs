using CondoHub.Domain.Interfaces.Repositorys;
using CondoHub.Domain.Interfaces.Services;
using CondoHub.Domain.Util;
using Microsoft.IdentityModel.Abstractions;

namespace CondoHub.Services.Services;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;

    public LogService(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public void RegisterLog(string message, TypeErrorLogEnum errorType)
    {
        try
        {
            _logRepository.AddLog(new LogEntry { Message = message });
        }
        catch
        {
            // logging must never break the calling operation
        }
    }
}
