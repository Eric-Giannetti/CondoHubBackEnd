using CondoHub.Domain.Entity.Log;

namespace CondoHub.Domain.Interfaces.Repositorys;

public interface ILogRepository
{
    void AddLog(LogEntry log);
}
