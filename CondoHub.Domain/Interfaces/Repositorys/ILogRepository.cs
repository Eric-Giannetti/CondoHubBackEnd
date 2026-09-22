// using CondoHub.Domain.Entity.Log;
using Microsoft.IdentityModel.Abstractions;

namespace CondoHub.Domain.Interfaces.Repositorys;

public interface ILogRepository
{
    void AddLog(LogEntry log);
}
